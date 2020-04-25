using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using MijnSauna.Backend.Common.Constants;
using MijnSauna.Backend.Sensors.Configuration;
using MijnSauna.Backend.Sensors.Interfaces;
using MijnSauna.Common.Protobuf;

namespace MijnSauna.Backend.Sensors
{
    public class SaunaSensor : ISaunaSensor
    {
        private readonly IConfigurationProxy _configurationProxy;

        private string _url;

        public SaunaSensor(
            IConfigurationProxy configurationProxy)
        {
            _configurationProxy = configurationProxy;
        }

        public async Task<int> GetTemperature()
        {
            try
            {
                await ReadConfiguration();

                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress(_url);
                var client = new SaunaService.SaunaServiceClient(channel);
                var request = new GetTemperatureRequest();
                var response = await client.GetTemperatureAsync(request);

                return response.Temperature;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task ReadConfiguration()
        {
            _url = await _configurationProxy.GetValue(ConfigurationConstants.SAUNASERVICE_HOST);
        }
    }
}