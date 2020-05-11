using System;
using System.Device.Gpio;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MijnSauna.Middleware.Processor.Controllers.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class GpioService : IGpioService
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IConfigurationService _configurationService;
        private readonly IGpioController _gpioController;
        private readonly ILogService _logService;

        private bool _initialized;

        public GpioService(
            IConfigurationService configurationService,
            IGpioController gpioController,
            ILogService logService)
        {
            _configurationService = configurationService;
            _gpioController = gpioController;
            _logService = logService;
        }

        public async Task Initialize()
        {
            try
            {
                await _semaphore.WaitAsync();

                if (!_initialized)
                {
                    _gpioController.OpenPin(_configurationService.SaunaOutputGpioPin, PinMode.Output);
                    _gpioController.Write(_configurationService.SaunaOutputGpioPin, PinValue.High);

                    _gpioController.OpenPin(_configurationService.InfraredOutputGpioPin, PinMode.Output);
                    _gpioController.Write(_configurationService.InfraredOutputGpioPin, PinValue.High);

                    _gpioController.OpenPin(_configurationService.SaunaInputGpioPin, PinMode.Input);
                    _gpioController.OpenPin(_configurationService.InfraredInputGpioPin, PinMode.Input);

                    _initialized = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public Task TurnSaunaOn()
        {
            return Write(_configurationService.SaunaOutputGpioPin, PinValue.Low);
        }

        public Task TurnSaunaOff()
        {
            return Write(_configurationService.SaunaOutputGpioPin, PinValue.High);
        }

        public Task<bool> IsSaunaOn()
        {
            return Read(_configurationService.SaunaInputGpioPin);
        }

        public Task TurnInfraredOn()
        {
            return Write(_configurationService.InfraredOutputGpioPin, PinValue.Low);
        }

        public Task TurnInfraredOff()
        {
            return Write(_configurationService.InfraredOutputGpioPin, PinValue.High);
        }

        public Task<bool> IsInfraredOn()
        {
            return Read(_configurationService.InfraredInputGpioPin);
        }

        public async Task<int> ReadTemperature()
        {
            var temperature = 0;

            try
            {
                await _semaphore.WaitAsync();

                var sensors = await File.ReadAllLinesAsync(_configurationService.TemperatureModulesW1);

                foreach (var sensor in sensors)
                {
                    var sensorFile = string.Format(_configurationService.TemperatureModuleW1, sensor);
                    if (File.Exists(sensorFile))
                    {
                        bool needRetry;
                        do
                        {
                            var sensorData = await File.ReadAllLinesAsync(sensorFile);
                            if (!sensorData[0].EndsWith("YES"))
                            {
                                needRetry = true;
                                await _logService.LogInformation(
                                    "CRC issue while reading temperature!",
                                    "There was a CRC issue while reading the temperature sensor! Retrying...");
                                await Task.Delay(500);
                                continue;
                            }

                            var temperatureData = sensorData[1].Substring(29, sensorData[1].Length - 29);
                            var rawTemperature = Convert.ToInt32(temperatureData);
                            temperature = (int)Math.Round((double)rawTemperature / 1000f);
                            needRetry = false;
                        } while (needRetry);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logService.LogException(
                    "Error while reading temperature!",
                    "Error while reading temperature!", ex);
            }
            finally
            {
                _semaphore.Release();
            }

            return temperature;
        }

        public async Task Shutdown()
        {
            try
            {
                await _semaphore.WaitAsync();

                if (_initialized)
                {
                    _gpioController.ClosePin(_configurationService.SaunaOutputGpioPin);
                    _gpioController.ClosePin(_configurationService.InfraredOutputGpioPin);
                    _gpioController.ClosePin(_configurationService.SaunaInputGpioPin);
                    _gpioController.ClosePin(_configurationService.InfraredInputGpioPin);

                    _initialized = false;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task Write(int pinNumber, PinValue value)
        {
            try
            {
                await _semaphore.WaitAsync();

                if (_initialized)
                {
                    _gpioController.Write(pinNumber, value);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<bool> Read(int pinNumber)
        {
            try
            {
                await _semaphore.WaitAsync();

                if (_initialized)
                {
                    return _gpioController.Read(pinNumber) == PinValue.Low;
                }

                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}