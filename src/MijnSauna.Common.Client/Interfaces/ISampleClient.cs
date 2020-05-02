using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface ISampleClient
    {
        Task<GetSamplesForSessionResponse> GetSamplesForSession(Guid sessionId);

        Task<CreateSampleForSessionResponse> CreateSampleForSession(Guid sessionId, CreateSampleForSessionRequest request);
    }
}