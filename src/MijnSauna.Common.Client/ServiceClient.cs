using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using RestSharp;

namespace MijnSauna.Common.Client
{
    public class ServiceClient : IServiceClient
    {
        private readonly IClientConfiguration _clientConfiguration;

        public ServiceClient(
            IClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
        }

        public async Task<TResponse> Get<TResponse>(string resource)
        {
            var client = new RestClient(_clientConfiguration.ServiceBaseUrl);
            var request = new RestRequest(resource, Method.GET);
            var response = await client.ExecuteAsync<TResponse>(request);
            return response.Data;
        }
    }
}