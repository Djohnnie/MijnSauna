using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Logic.Interfaces
{
    public interface ISampleLogic : ILogic
    {
        Task<GetSamplesForSessionResponse> GetSamplesForSession(Guid sessionId);

        Task<CreateSampleForSessionResponse> CreateSampleForSession(Guid sessionId, CreateSampleForSessionRequest request);
    }
}