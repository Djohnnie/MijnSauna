using System;
using Microsoft.Extensions.Logging;
using MijnSauna.Middleware.Processor.Context.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class LoggerService<TCategoryName> : ILoggerService<TCategoryName>
    {
        private readonly ISessionContext _sessionContext;
        private readonly ILogger<TCategoryName> _logger;

        public LoggerService(
            ISessionContext sessionContext,
            ILogger<TCategoryName> logger)
        {
            _sessionContext = sessionContext;
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionContext.GetSessionId(), _sessionContext.GetCorrelationId(), message.ToUpperInvariant());
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionContext.GetSessionId(), _sessionContext.GetCorrelationId(), message.ToUpperInvariant());
        }

        public void LogError(string message)
        {
            _logger.LogError(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionContext.GetSessionId(), _sessionContext.GetCorrelationId(), message.ToUpperInvariant());
        }

        private string TimeStamp()
        {
            return $"[{DateTime.UtcNow:dd/MM HH:mm:ss}]";
        }
    }
}