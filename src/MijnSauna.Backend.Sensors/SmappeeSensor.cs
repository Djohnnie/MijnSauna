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
    public class SmappeeSensor : ISmappeeSensor
    {
        private readonly IConfigurationProxy _configurationProxy;

        private string _loginUrl;
        private string _reportUrl;
        private string _logoutUrl;
        private string _admin;
        private string _regex;

        public SmappeeSensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<int> GetPowerUsage()
        {
            await ReadConfiguration();
            await Login();
            var result = await Report();
            await Logout();

            return result;
        }

        private async Task Login()
        {
            var client = new RestClient(_loginUrl);
            var request = new RestRequest(Method.POST);
            request.Body = new RequestBody("application/json", "", _admin);
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
                foreach (Match m in Regex.Matches(response.Content, _regex))
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
            request.Body = new RequestBody("application/json", "", _admin);
            var response = await client.ExecuteAsync(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
        }

        private async Task ReadConfiguration()
        {
            var hostBase = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_HOST_BASE);
            var loginUrl = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_LOGON_RESOURCE);
            _loginUrl = $"{hostBase}/{loginUrl}";
            var reportUrl = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_REPORT_RESOURCE);
            _reportUrl = $"{hostBase}/{reportUrl}";
            var logoffUrl = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_LOGOFF_RESOURCE);
            _logoutUrl = $"{hostBase}/{logoffUrl}";
            _admin = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_ADMIN);
            _regex = await _configurationProxy.GetString(ConfigurationConstants.SMAPPEE_REGEX);
        }
    }
}