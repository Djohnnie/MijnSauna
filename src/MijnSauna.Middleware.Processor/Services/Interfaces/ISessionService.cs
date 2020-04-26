using System;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface ISessionService
    {
        bool IsActive();

        Guid GetSessionId();
        
        void UpdateSession(GetActiveSessionResponse activeSession);

        bool KillSession();
    }
}