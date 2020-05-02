using System;
using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Common.Client
{
    public class SampleClient : ISampleClient
    {
        private readonly IServiceClient _serviceClient;

        public SampleClient(
            IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Task<GetSamplesForSessionResponse> GetSamplesForSession(Guid sessionId)
        {
            return _serviceClient.Get<GetSamplesForSessionResponse>($"sauna/sessions/{sessionId}/samples");
        }

        public Task<CreateSampleForSessionResponse> CreateSampleForSession(Guid sessionId, CreateSampleForSessionRequest request)
        {
            return _serviceClient.Post<CreateSampleForSessionResponse, CreateSampleForSessionRequest>(
                $"sauna/sessions/{sessionId}/samples", request);
        }
    }
}