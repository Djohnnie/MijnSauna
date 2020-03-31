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
    public class SmappeeSensor : ISmappeeSensor
    {
        private readonly IConfigurationProxy _configurationProxy;

        private readonly string _loginUrl = "";
        private readonly string _reportUrl = "";
        private readonly string _logoutUrl = "";

        public SmappeeSensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<int> GetPowerUsage()
        {
            await Login();
            var result = await Report();
            await Logout();

            return result;
        }

        private async Task Login()
        {
            var client = new RestClient(_loginUrl);
            var request = new RestRequest(Method.POST);
            request.Body = new RequestBody("application/json", "", "");
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
        }

        private async Task<int> Report()
        {
            int result = 0;

            var client = new RestClient(_reportUrl);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                foreach (Match m in Regex.Matches(response.Content, @"activePower=(\d*\.?\d+) W"))
                {
                    result = (int)Math.Round(Convert.ToDecimal(m.Groups[1].Value, CultureInfo.InvariantCulture));
                }
            }

            return result;
        }

        private async Task Logout()
        {
            var client = new RestClient(_logoutUrl);
            var request = new RestRequest(Method.POST);
            request.Body = new RequestBody("application/json", "", "");
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
        }
    }
}