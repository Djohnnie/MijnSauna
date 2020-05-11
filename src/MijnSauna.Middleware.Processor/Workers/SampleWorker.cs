using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Samples;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class SampleWorker : BackgroundService
    {
        private readonly ISessionService _sessionService;
        private readonly ISampleClient _sampleClient;
        private readonly IGpioService _gpioService;
        private readonly ILogService _logService;
        private readonly ILoggerService<SampleWorker> _logger;

        public SampleWorker(
            ISessionService sessionService,
            ISampleClient sampleClient,
            IGpioService gpioService,
            ILogService logService,
            ILoggerService<SampleWorker> logger)
        {
            _sessionService = sessionService;
            _sampleClient = sampleClient;
            _gpioService = gpioService;
            _logService = logService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

                _logger.LogInformation($"{nameof(SampleWorker)} started!");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                        if (_sessionService.IsActive())
                        {
                            var sessionId = _sessionService.GetSessionId();
                            if (sessionId.HasValue)
                            {
                                var sampleRequest = new CreateSampleForSessionRequest
                                {
                                    IsSaunaPowered = await _gpioService.IsSaunaOn(),
                                    IsInfraredPowered = await _gpioService.IsInfraredOn(),
                                    Temperature = await _gpioService.ReadTemperature(),
                                    TimeStamp = DateTime.UtcNow
                                };

                                await _sampleClient.CreateSampleForSession(sessionId.Value, sampleRequest);

                                _logger.LogInformation("Sample for active session sent.");
                            }
                            else
                            {
                                _logger.LogInformation("No active session.");
                            }
                        }
                        else
                        {
                            _logger.LogInformation("No active session.");
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // This is most likely due to the Task.Delay being cancelled.
                    }
                }

                _logger.LogInformation($"{nameof(SampleWorker)} stopped!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SampleWorker)} throws Exception: {ex.Message}!"); 
                await _logService.LogException(
                    "SessionWorker throws Exception!",
                    "SessionWorker throws Exception!", ex);
            }
        }
    }
}