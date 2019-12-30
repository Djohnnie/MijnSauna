using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Configuration;
using MijnSauna.Common.DataTransferObjects.Samples;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface IBackendService
    {
        Task<GetConfigurationValuesResponse> GetConfigurationValues();

        Task<GetActiveSessionResponse> GetActiveSession();

        Task CreateSampleForSession(Guid sessionId, CreateSampleForSessionRequest createSampleForSessionRequest);
    }
}