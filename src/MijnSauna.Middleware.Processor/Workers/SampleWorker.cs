using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MijnSauna.Common.DataTransferObjects.Samples;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class SampleWorker : BackgroundService
    {
        private readonly ISessionService _sessionService;
        private readonly IBackendService _backendService;
        private readonly IGpioService _gpioService;
        private readonly ILogger<SampleWorker> _logger;

        public SampleWorker(
            ISessionService sessionService,
            IBackendService backendService,
            IGpioService gpioService,
            ILogger<SampleWorker> logger)
        {
            _sessionService = sessionService;
            _backendService = backendService;
            _gpioService = gpioService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
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

                    await _backendService.CreateSampleForSession(sessionId, sampleRequest);
                }

                _logger.LogInformation($"Configuration updated at {DateTimeOffset.UtcNow}");
            }
        }
    }
}