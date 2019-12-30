using System;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class SessionService : ISessionService
    {
        private readonly IGpioService _gpioService;

        private Guid? _sessionId;

        public SessionService(
            IGpioService gpioService)
        {
            _gpioService = gpioService;
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
                _gpioService.TurnSaunaOn();
            }

            if (!activeSession.IsSauna && _gpioService.IsSaunaOn())
            {
                _gpioService.TurnSaunaOff();
            }

            if (activeSession.IsInfrared && !_gpioService.IsInfraredOn())
            {
                _gpioService.TurnInfraredOn();
            }

            if (!activeSession.IsInfrared && _gpioService.IsInfraredOn())
            {
                _gpioService.TurnInfraredOff();
            }
        }

        public void KillSession()
        {
            _sessionId = null;
            _gpioService.TurnSaunaOff();
            _gpioService.TurnInfraredOff();
        }
    }
}