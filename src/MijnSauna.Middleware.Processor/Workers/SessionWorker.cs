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
                var activeSession = await _backendService.GetActiveSession();
                if (activeSession == null)
                {
                    
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}