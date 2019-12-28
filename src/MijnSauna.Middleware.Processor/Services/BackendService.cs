using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Services.Interfaces;
using RestSharp;

namespace MijnSauna.Middleware.Processor.Services
{
    public class BackendService : IBackendService
    {
        private readonly RestClient _client;

        public BackendService()
        {
            _client = new RestClient("https://localhost:5000");
        }

        public async Task<GetActiveSessionResponse> GetActiveSession()
        {
            var request = new RestRequest("sauna/sessions/active");
            request.AddHeader("mijn-sauna", "97795bd8-e606-4085-9950-72fa35896dca");
            var response = await _client.ExecuteGetTaskAsync<GetActiveSessionResponse>(request);

            return response.IsSuccessful ? response.Data : null;
        }
    }
}