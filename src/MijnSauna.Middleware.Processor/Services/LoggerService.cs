using System;
using Microsoft.Extensions.Logging;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class LoggerService<TCategoryName> : ILoggerService<TCategoryName>
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<TCategoryName> _logger;

        public LoggerService(
            ISessionService sessionService,
            ILogger<TCategoryName> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionService.GetSessionId(), _sessionService.GetCorrelationId(), message.ToUpperInvariant());
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionService.GetSessionId(), _sessionService.GetCorrelationId(), message.ToUpperInvariant());
        }

        public void LogError(string message)
        {
            _logger.LogError(
                "{timestamp} {sessionId} {correlationId} {description}", 
                TimeStamp(), _sessionService.GetSessionId(), _sessionService.GetCorrelationId(), message.ToUpperInvariant());
        }

        private string TimeStamp()
        {
            return $"[{DateTime.UtcNow:dd/MM HH:mm:ss}]";
        }
    }
}