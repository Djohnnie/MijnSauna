using System;
using MijnSauna.Middleware.Processor.Context.Interfaces;

namespace MijnSauna.Middleware.Processor.Context
{
    public class SessionContext : ISessionContext
    {
        private readonly Guid _correlationId = Guid.NewGuid();
        private Guid? _sessionId;

        public void SetSessionId(Guid? sessionId)
        {
            _sessionId = sessionId;
        }

        public Guid? GetSessionId()
        {
            return _sessionId;
        }

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }
    }
}