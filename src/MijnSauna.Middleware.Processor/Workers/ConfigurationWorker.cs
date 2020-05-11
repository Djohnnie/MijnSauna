using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Workers
{
    public class ConfigurationWorker : BackgroundService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IConfigurationClient _configurationClient;
        private readonly ILogService _logService;
        private readonly ILoggerService<ConfigurationWorker> _logger;

        public ConfigurationWorker(
            IConfigurationService configurationService,
            IConfigurationClient configurationClient,
            ILogService logService,
            ILoggerService<ConfigurationWorker> logger)
        {
            _configurationService = configurationService;
            _configurationClient = configurationClient;
            _logService = logService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                _logger.LogInformation($"{nameof(ConfigurationWorker)} started!");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var configuration = await _configurationClient.GetConfigurationValues();

                        if (configuration != null)
                        {
                            _configurationService.UpdateConfiguration(configuration);
                            _logger.LogInformation("Configuration updated.");
                        }
                        else
                        {
                            _logger.LogError("Configuration error.");
                        }

                        await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                    }
                    catch (TaskCanceledException)
                    {
                        // This is most likely due to the Task.Delay being cancelled.
                    }
                }

                _logger.LogInformation($"{nameof(ConfigurationWorker)} stopped!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ConfigurationWorker)} throws Exception: {ex.Message}!");
                await _logService.LogException(
                    "SessionWorker throws Exception!",
                    "SessionWorker throws Exception!", ex);
            }
        }
    }
}