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
        private readonly ILoggerService<SampleWorker> _logger;

        public SampleWorker(
            ISessionService sessionService,
            ISampleClient sampleClient,
            IGpioService gpioService,
            ILoggerService<SampleWorker> logger)
        {
            _sessionService = sessionService;
            _sampleClient = sampleClient;
            _gpioService = gpioService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try 
            { 
                _logger.LogInformation($"{nameof(SampleWorker)} started!");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                        if (_sessionService.IsActive())
                        {
                            var sessionId = _sessionService.GetSessionId();
                            var sampleRequest = new CreateSampleForSessionRequest
                            {
                                IsSaunaPowered = _gpioService.IsSaunaOn(),
                                IsInfraredPowered = _gpioService.IsInfraredOn(),
                                Temperature = await _gpioService.ReadTemperature(),
                                TimeStamp = DateTime.UtcNow
                            };

                            await _sampleClient.CreateSampleForSession(sessionId, sampleRequest);

                            _logger.LogInformation("Sample for active session sent.");
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
            }
        }
    }
}