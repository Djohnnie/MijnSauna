using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface ISessionService
    {
        bool IsActive();

        Guid? GetSessionId();
        
        Task UpdateSession(GetActiveSessionResponse activeSession);

        Task<bool> KillSession();
    }
}