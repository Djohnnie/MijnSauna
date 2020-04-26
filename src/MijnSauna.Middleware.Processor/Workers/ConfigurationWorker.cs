using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class ConfigurationWorker : BackgroundService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IConfigurationClient _configurationClient;
        private readonly ILogger<ConfigurationWorker> _logger;

        public ConfigurationWorker(
            IConfigurationService configurationService,
            IConfigurationClient configurationClient,
            ILogger<ConfigurationWorker> logger)
        {
            _configurationService = configurationService;
            _configurationClient = configurationClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"{nameof(ConfigurationWorker)} started at {DateTime.UtcNow}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var configuration = await _configurationClient.GetConfigurationValues();

                    if (configuration != null)
                    {
                        _configurationService.UpdateConfiguration(configuration);
                        _logger.LogInformation($"Configuration updated at {DateTime.UtcNow}");
                    }
                    else
                    {
                        _logger.LogError($"Configuration error {DateTime.UtcNow}");
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }

                _logger.LogInformation($"{nameof(ConfigurationWorker)} stopped at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ConfigurationWorker)} throws Exception {ex.Message} at {DateTime.UtcNow}");
            }
        }
    }
}