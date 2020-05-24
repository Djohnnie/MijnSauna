using System;

namespace MijnSauna.Middleware.Processor.Context.Interfaces
{
    public interface ISessionContext
    {
        void SetSessionId(Guid? sessionId);

        Guid? GetSessionId();

        Guid GetCorrelationId();
    }
}