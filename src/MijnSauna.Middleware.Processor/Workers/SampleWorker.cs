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
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_sessionService.IsActive())
                {
                    var sampleRequest = new CreateSampleForSessionRequest
                    {
                        //IsInfraredPowered = _sessionService.IsInfraredPowered(),
                        //IsSaunaPowered = _sessionService.IsSaunaPowered(),
                        //Temperature = _gpioService.GetTemperature(),
                        TimeStamp = DateTime.UtcNow
                    };

                    //await _backendService.CreateSampleForSession(_sessionService.SessionId, sampleRequest);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}