using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class SessionWorker : BackgroundService
    {
        private readonly ISessionService _sessionService;
        private readonly ISessionClient _sessionClient;
        private readonly IGpioService _gpioService;
        private readonly ILogger<SessionWorker> _logger;

        public SessionWorker(
            ISessionService sessionService,
            ISessionClient sessionClient,
            IGpioService gpioService,
            ILogger<SessionWorker> logger)
        {
            _sessionService = sessionService;
            _sessionClient = sessionClient;
            _gpioService = gpioService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"{nameof(SessionWorker)} started at {DateTimeOffset.UtcNow}");

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                _gpioService.Initialize();
                _logger.LogInformation($"GPIO initialized at {DateTimeOffset.UtcNow}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                    var activeSession = await _sessionClient.GetActiveSession();
                    if (activeSession != null)
                    {
                        _sessionService.UpdateSession(activeSession);
                        _logger.LogInformation($"Active session updated at {DateTimeOffset.UtcNow}");
                    }
                    else
                    {
                        _sessionService.KillSession();
                        _logger.LogInformation($"Active session killed at {DateTimeOffset.UtcNow}");
                    }

                }

                _gpioService.Shutdown();
                _logger.LogInformation($"GPIO shutdown at {DateTimeOffset.UtcNow}");

                _logger.LogInformation($"{nameof(SessionWorker)} stopped at {DateTimeOffset.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SessionWorker)} throws Exception {ex.Message} at {DateTimeOffset.UtcNow}");
            }
        }
    }
}