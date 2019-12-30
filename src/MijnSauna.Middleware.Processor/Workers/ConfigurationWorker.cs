using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class ConfigurationWorker : BackgroundService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IBackendService _backendService;
        private readonly ILogger<ConfigurationWorker> _logger;

        public ConfigurationWorker(
            IConfigurationService configurationService,
            IBackendService backendService,
            ILogger<ConfigurationWorker> logger)
        {
            _configurationService = configurationService;
            _backendService = backendService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var configuration = await _backendService.GetConfigurationValues();
                _configurationService.UpdateConfiguration(configuration);

                _logger.LogInformation($"Configuration updated at {DateTimeOffset.UtcNow}");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}