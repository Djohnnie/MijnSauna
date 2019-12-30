using System;
using Microsoft.Extensions.Logging;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Helpers;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class SessionService : ISessionService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IGpioService _gpioService;
        private readonly ILogger<SessionService> _logger;

        private Guid? _sessionId;
        private TemperatureTrend _temperatureTrend = TemperatureTrend.Idle;

        public SessionService(
            IConfigurationService configurationService,
            IGpioService gpioService,
            ILogger<SessionService> logger)
        {
            _configurationService = configurationService;
            _gpioService = gpioService;
            _logger = logger;
        }

        public bool IsActive()
        {
            return _sessionId.HasValue;
        }

        public Guid GetSessionId()
        {
            return _sessionId.Value;
        }

        public void UpdateSession(GetActiveSessionResponse activeSession)
        {
            _sessionId = activeSession.SessionId;

            if (activeSession.IsSauna && !_gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Sauna should be turned on and is off right now!");
                _gpioService.TurnSaunaOn();
            }

            if (!activeSession.IsSauna && _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Sauna should be turned off and is on right now!");
                _gpioService.TurnSaunaOff();
            }

            if (activeSession.IsInfrared && !_gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Infrared should be turned on and is off right now!");
                _gpioService.TurnInfraredOn();
            }

            if (!activeSession.IsInfrared && _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Infrared should be turned off and is on right now!");
                _gpioService.TurnInfraredOff();
            }
        }

        public void KillSession()
        {
            _sessionId = null;
            _gpioService.TurnSaunaOff();
            _gpioService.TurnInfraredOff();
            _temperatureTrend = TemperatureTrend.Idle;
        }
    }
}