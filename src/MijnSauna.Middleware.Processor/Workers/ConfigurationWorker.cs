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
            try
            {
                _logger.LogInformation($"{nameof(ConfigurationWorker)} started at {DateTimeOffset.UtcNow}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var configuration = await _backendService.GetConfigurationValues();

                    if (configuration != null)
                    {
                        _configurationService.UpdateConfiguration(configuration);
                        _logger.LogInformation($"Configuration updated at {DateTimeOffset.UtcNow}");
                    }
                    else
                    {
                        _logger.LogError($"Configuration error {DateTimeOffset.UtcNow}");
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }

                _logger.LogInformation($"{nameof(ConfigurationWorker)} stopped at {DateTimeOffset.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ConfigurationWorker)} throws Exception {ex.Message} at {DateTimeOffset.UtcNow}");
            }
        }
    }
}