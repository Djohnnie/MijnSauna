using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class SessionService : ISessionService
    {
        private readonly IGpioService _gpioService;
        private readonly ILoggerService<SessionService> _logger;

        private readonly Guid _correlationId = Guid.NewGuid();
        private Guid? _sessionId;

        public SessionService(
            IGpioService gpioService,
            ILoggerService<SessionService> logger)
        {
            _gpioService = gpioService;
            _logger = logger;
        }

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }

        public bool IsActive()
        {
            return _sessionId.HasValue;
        }

        public Guid? GetSessionId()
        {
            return _sessionId;
        }

        public async Task UpdateSession(GetActiveSessionResponse activeSession)
        {
            _sessionId = activeSession.SessionId;

            var temperature = await _gpioService.ReadTemperature();

            if (activeSession.IsSauna && !await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires sauna but sauna is off!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                if (temperature < activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Sauna should be turned on!");
                    await _gpioService.TurnSaunaOn();
                }
            }

            if (activeSession.IsSauna && await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires sauna and sauna is on!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                if (temperature >= activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Sauna should be turned off!");
                    await _gpioService.TurnSaunaOff();
                }
            }

            if (!activeSession.IsSauna && await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires no sauna and sauna is on!");
                _logger.LogInformation("Sauna should be turned off!");
                await _gpioService.TurnSaunaOff();
            }

            if (activeSession.IsInfrared && !await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires infrared but infrared is off!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                if (temperature < activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Infrared should be turned on!");
                    await _gpioService.TurnInfraredOn();
                }
            }

            if (activeSession.IsInfrared && await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires infrared and infrared is on!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                if (temperature >= activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Infrared should be turned off!");
                    await _gpioService.TurnInfraredOff();
                }
            }

            if (!activeSession.IsInfrared && await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires no infrared and infrared is on!");
                _logger.LogInformation("Infrared should be turned off!");
                await _gpioService.TurnInfraredOff();
            }
        }

        public async Task<bool> KillSession()
        {
            var result = IsActive();

            _sessionId = null;
            await _gpioService.TurnSaunaOff();
            await _gpioService.TurnInfraredOff();

            return result;
        }
    }
}