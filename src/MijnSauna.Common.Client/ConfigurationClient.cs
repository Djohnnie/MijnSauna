using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Configuration;

namespace MijnSauna.Common.Client
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly IServiceClient _serviceClient;

        public ConfigurationClient(
            IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Task<GetConfigurationValuesResponse> GetConfigurationValues()
        {
            return _serviceClient.Get<GetConfigurationValuesResponse>("configuration");
        }
    }
}