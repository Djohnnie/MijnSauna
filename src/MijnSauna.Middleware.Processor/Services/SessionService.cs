using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Context.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionContext _sessionContext;
        private readonly IGpioService _gpioService;
        private readonly ILoggerService<SessionService> _logger;

        private Guid? _sessionId;

        public SessionService(
            ISessionContext sessionContext,
            IGpioService gpioService,
            ILoggerService<SessionService> logger)
        {
            _sessionContext = sessionContext;
            _gpioService = gpioService;
            _logger = logger;
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
            SetSessionId(activeSession.SessionId);

            // Read the current temperature inside the sauna booth.
            var temperature = await _gpioService.ReadTemperature();

            // If a sauna session should be active and the sauna GPIO is not turned on...
            if (activeSession.IsSauna && !await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires sauna but sauna is off!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                // If the current temperature is below the temperature goal...
                if (temperature < activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Sauna should be turned on!");
                    await _gpioService.TurnSaunaOn();
                }
            }

            // If a sauna session should be active and the sauna GPIO is turned on...
            if (activeSession.IsSauna && await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires sauna and sauna is on!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                // If the current temperature is equal or higher then the temperature goal...
                if (temperature >= activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Sauna should be turned off!");
                    await _gpioService.TurnSaunaOff();
                }
            }

            // If a sauna session should not be active and the sauna GPIO is turned on...
            if (!activeSession.IsSauna && await _gpioService.IsSaunaOn())
            {
                _logger.LogInformation("Active session requires no sauna and sauna is on!");
                _logger.LogInformation("Sauna should be turned off!");
                await _gpioService.TurnSaunaOff();
            }

            // If an infrared session should be active and the infrared GPIO is not turned on...
            if (activeSession.IsInfrared && !await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires infrared but infrared is off!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                // If the current temperature is lower then the temperature goal...
                if (temperature < activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Infrared should be turned on!");
                    await _gpioService.TurnInfraredOn();
                }
            }

            // If an infrared session should be active and the sauna GPIO is turned on...
            if (activeSession.IsInfrared && await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires infrared and infrared is on!");
                _logger.LogInformation($"Temperature goal is {activeSession.TemperatureGoal} and actual temperature is {temperature}.");

                // If the current temperature is equal or higher then the temperature goal...
                if (temperature >= activeSession.TemperatureGoal)
                {
                    _logger.LogInformation("Infrared should be turned off!");
                    await _gpioService.TurnInfraredOff();
                }
            }

            // If an infrared session should not be active and the sauna GPIO is turned on...
            if (!activeSession.IsInfrared && await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires no infrared and infrared is on!");
                _logger.LogInformation("Infrared should be turned off!");
                await _gpioService.TurnInfraredOff();
            }

            // If a sauna session should be active and the infrared boost is not turned on...
            if (activeSession.IsSauna && temperature < 50 && !await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires sauna and can benefit from infrared boost!");
                await _gpioService.TurnInfraredOn();
            }

            // If a sauna session should be active and the infrared boost is turned on...
            if (activeSession.IsSauna && temperature >= 50 && await _gpioService.IsInfraredOn())
            {
                _logger.LogInformation("Active session requires sauna and should stop boosting from infrared!");
                await _gpioService.TurnInfraredOff();
            }
        }

        public async Task<bool> KillSession()
        {
            var result = IsActive();

            SetSessionId(null);
            await _gpioService.TurnSaunaOff();
            await _gpioService.TurnInfraredOff();

            return result;
        }

        private void SetSessionId(Guid? sessionId)
        {
            _sessionId = sessionId;
            _sessionContext.SetSessionId(sessionId);
        }
    }
}