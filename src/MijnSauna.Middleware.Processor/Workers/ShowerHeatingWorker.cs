using Microsoft.Extensions.Hosting;
using MijnSauna.Middleware.Processor.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MijnSauna.Middleware.Processor.Workers;

public class ShowerHeatingWorker : BackgroundService
{
    private readonly IConfigurationService _configurationService;
    private readonly IGpioService _gpioService;
    private readonly ILogService _logService;
    private readonly ILoggerService<ShowerHeatingWorker> _logger;

    public ShowerHeatingWorker(
        IConfigurationService configurationService,
        IGpioService gpioService,
        ILogService logService,
        ILoggerService<ShowerHeatingWorker> logger)
    {
        _configurationService = configurationService;
        _gpioService = gpioService;
        _logService = logService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            var log = $"{nameof(ShowerHeatingWorker)} started!";
            _logger.LogInformation(log);
            await _logService.LogInformation(nameof(ShowerHeatingWorker), log);

            await _gpioService.Initialize();
            _logger.LogInformation("GPIO initialized.");

            var showerHeatingIsOn = false;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);

                    var temperature = await _gpioService.ReadTemperature();

                    if (temperature < _configurationService.ShowerHeatingThresholdTemperature)
                    {
                        await _gpioService.TurnShowerHeatingOn();
                        showerHeatingIsOn = true;
                        _logger.LogInformation("Shower heating turned ON.");
                        await _logService.LogInformation(nameof(ShowerHeatingWorker), "Shower heating turned ON.");
                    }
                    else
                    {
                        await _gpioService.TurnShowerHeatingOff();

                        if (showerHeatingIsOn)
                        {
                            _logger.LogInformation("Shower heating turned OFF.");
                            await _logService.LogInformation(nameof(ShowerHeatingWorker), "Shower heating turned OFF.");
                        }

                        showerHeatingIsOn = false;
                    }
                }
                catch (TaskCanceledException)
                {
                    // This is most likely due to the Task.Delay being cancelled.
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogError($"{nameof(ShowerHeatingWorker)} throws Exception: {ex.Message}!");
                    await _logService.LogException(nameof(ShowerHeatingWorker), $"{nameof(ShowerHeatingWorker)} throws Exception!", ex);
                }
            }

            await _gpioService.Shutdown();
            _logger.LogInformation("GPIO shutdown.");

            _logger.LogInformation($"{nameof(ShowerHeatingWorker)} stopped!");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(ShowerHeatingWorker)} throws Exception: {ex.Message}!");
            await _logService.LogException(nameof(ShowerHeatingWorker), $"{nameof(ShowerHeatingWorker)} throws Exception!", ex);
        }
    }
}