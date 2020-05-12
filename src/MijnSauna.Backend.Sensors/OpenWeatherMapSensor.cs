using MijnSauna.Backend.Common.Constants;
using MijnSauna.Backend.Sensors.Configuration;
using MijnSauna.Backend.Sensors.Interfaces;
using RestSharp;
using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors
{
    public class OpenWeatherMapSensor : IOpenWeatherMapSensor
    {
        private readonly IConfigurationProxy _configurationProxy;

        private string _url;
        private string _regex;

        public OpenWeatherMapSensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<int> GetOutsideTemperature()
        {
            await ReadConfiguration();

            var result = 0;

            var client = new RestClient(_url);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                foreach (Match m in Regex.Matches(response.Content, _regex))
                {
                    result = (int)Math.Round(Convert.ToDecimal(m.Groups[1].Value, CultureInfo.InvariantCulture));
                }
            }

            return result;
        }

        private async Task ReadConfiguration()
        {
            var hostBase = await _configurationProxy.GetString(ConfigurationConstants.OPENWEATHERMAP_HOST_BASE);
            var cityId = await _configurationProxy.GetString(ConfigurationConstants.OPENWEATHERMAP_CITY_ID);
            var appId = await _configurationProxy.GetString(ConfigurationConstants.OPENWEATHERMAP_CLIENT_ID);
            _regex = await _configurationProxy.GetString(ConfigurationConstants.OPENWEATHERMAP_REGEX);
            _url = $"{hostBase}?id={cityId}&appid={appId}&units=metric";
        }
    }
}