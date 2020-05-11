using System.Net;
using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects;
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

        public async Task<TResponse> Get<TResponse>(string resource) where TResponse : new()
        {
            var client = new RestClient(_clientConfiguration.ServiceBaseUrl);
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("ClientId", _clientConfiguration.ClientId);
            var response = await client.ExecuteAsync<ApiResult<TResponse>>(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data.Content;
            }

            return default;
        }

        public async Task Post<TBody>(string resource, TBody body)
        {
            var client = new RestClient(_clientConfiguration.ServiceBaseUrl);
            var request = new RestRequest(resource, Method.POST);
            request.AddHeader("ClientId", _clientConfiguration.ClientId);
            request.AddJsonBody(body);
            var response = await client.ExecuteAsync<ApiResult>(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
            }
        }

        public async Task<TResponse> Post<TResponse, TBody>(string resource, TBody body) where TResponse : new()
        {
            var client = new RestClient(_clientConfiguration.ServiceBaseUrl);
            var request = new RestRequest(resource, Method.POST);
            request.AddHeader("ClientId", _clientConfiguration.ClientId);
            request.AddJsonBody(body);
            var response = await client.ExecuteAsync<ApiResult<TResponse>>(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data.Content;
            }

            return default;
        }

        public async Task Put(string resource)
        {
            var client = new RestClient(_clientConfiguration.ServiceBaseUrl);
            var request = new RestRequest(resource, Method.PUT);
            request.AddHeader("ClientId", _clientConfiguration.ClientId);
            await client.ExecuteAsync<ApiResult>(request);
        }
    }
}