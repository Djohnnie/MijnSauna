using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class SessionWorker : BackgroundService
    {
        private readonly ISessionService _sessionService;
        private readonly ISessionClient _sessionClient;
        private readonly IGpioService _gpioService;
        private readonly ILogService _logService;
        private readonly ILoggerService<SessionWorker> _logger;

        public SessionWorker(
            ISessionService sessionService,
            ISessionClient sessionClient,
            IGpioService gpioService,
            ILogService logService,
            ILoggerService<SessionWorker> logger)
        {
            _sessionService = sessionService;
            _sessionClient = sessionClient;
            _gpioService = gpioService;
            _logService = logService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

                _logger.LogInformation($"{nameof(SessionWorker)} started!");

                await _gpioService.Initialize();
                _logger.LogInformation("GPIO initialized.");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                        var activeSession = await _sessionClient.GetActiveSession();
                        if (activeSession != null)
                        {
                            await _sessionService.UpdateSession(activeSession);
                            _logger.LogInformation("Active session updated.");
                        }
                        else
                        {
                            var isKilled = await _sessionService.KillSession();
                            if (isKilled)
                            {
                                _logger.LogInformation("Active session killed.");
                            }
                            else
                            {
                                _logger.LogInformation("No active session.");
                            }
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // This is most likely due to the Task.Delay being cancelled.
                    }
                }

                await _gpioService.Shutdown();
                _logger.LogInformation("GPIO shutdown.");

                _logger.LogInformation($"{nameof(SessionWorker)} stopped!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SessionWorker)} throws Exception: {ex.Message}!");
                await _logService.LogException(
                    "SessionWorker throws Exception!",
                    "SessionWorker throws Exception!", ex);
            }
        }
    }
}