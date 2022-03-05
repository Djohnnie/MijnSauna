using MijnSauna.Backend.Common.Constants;
using MijnSauna.Backend.Sensors.Configuration;
using MijnSauna.Backend.Sensors.Interfaces;
using MijnSauna.Backend.Sensors.Model;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;
using System.Net;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors
{
    public class ShellySensor : IShellySensor
    {
        private readonly IConfigurationProxy _configurationProxy;

        public ShellySensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<(decimal, decimal)> GetPowerUsage()
        {
            var host = await _configurationProxy.GetString(ConfigurationConstants.SHELLY_HOST_BASE);
            var client = new RestClient(host);
            var request = new RestRequest("status", Method.Get);
            var response = await client.ExecuteAsync<ShellyEMetering>(request);

            if (response.ResponseStatus == ResponseStatus.Completed && 
                response.StatusCode == HttpStatusCode.OK && 
                response.Data != null && 
                response.Data.Meters.Count == 2)
            {
                return (response.Data.Meters[0].Power, response.Data.Meters[1].Power);
            }

            return (0M, 0M);
        }
    }
}