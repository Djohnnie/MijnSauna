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

        private string _url = "";

        public OpenWeatherMapSensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<int> GetOutsideTemperature()
        {
            var result = 0;

            var client = new RestClient(_url);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                foreach (Match m in Regex.Matches(response.Content, @"""temp"":(\d*\.?\d+),"))
                {
                    result = (int)Math.Round(Convert.ToDecimal(m.Groups[1].Value, CultureInfo.InvariantCulture));
                }
            }

            return result;
        }
    }
}