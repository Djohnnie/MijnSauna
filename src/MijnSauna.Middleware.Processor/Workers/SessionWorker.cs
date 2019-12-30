using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class SessionWorker : BackgroundService
    {
        private readonly ISessionService _sessionService;
        private readonly IBackendService _backendService;
        private readonly IGpioService _gpioService;
        private readonly ILogger<SessionWorker> _logger;

        public SessionWorker(
            ISessionService sessionService,
            IBackendService backendService,
            IGpioService gpioService,
            ILogger<SessionWorker> logger)
        {
            _sessionService = sessionService;
            _backendService = backendService;
            _gpioService = gpioService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                var activeSession = await _backendService.GetActiveSession();
                if (activeSession != null)
                {
                    _sessionService.UpdateSession(activeSession);
                }
                else
                {
                    _sessionService.KillSession();
                }

                _logger.LogInformation($"Active session polled at {DateTimeOffset.UtcNow}");
            }
        }
    }
}