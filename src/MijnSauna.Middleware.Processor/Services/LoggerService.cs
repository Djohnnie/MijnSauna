using System;
using Microsoft.Extensions.Logging;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class LoggerService<TCategoryName> : ILoggerService<TCategoryName>
    {
        private readonly ILogger<TCategoryName> _logger;

        public LoggerService(ILogger<TCategoryName> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation($"{TimeStamp()} {message.ToUpperInvariant()}");
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning($"{TimeStamp()} {message.ToUpperInvariant()}");
        }

        public void LogError(string message)
        {
            _logger.LogError($"{TimeStamp()} {message.ToUpperInvariant()}");
        }

        private string TimeStamp()
        {
            return $"[{DateTime.UtcNow:dd/MM HH:mm:ss}]";
        }
    }
}