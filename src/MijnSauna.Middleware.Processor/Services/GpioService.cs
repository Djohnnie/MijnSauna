using System;
using System.Device.Gpio;
using System.IO;
using System.Threading.Tasks;
using MijnSauna.Middleware.Processor.Controllers.Interfaces;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class GpioService : IGpioService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IGpioController _gpioController;

        private bool _initialized;

        public GpioService(
            IConfigurationService configurationService,
            IGpioController gpioController)
        {
            _configurationService = configurationService;
            _gpioController = gpioController;
        }

        public void Initialize()
        {
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

        public void TurnSaunaOn()
        {
            Write(_configurationService.SaunaOutputGpioPin, PinValue.Low);
        }

        public void TurnSaunaOff()
        {
            Write(_configurationService.SaunaOutputGpioPin, PinValue.High);
        }

        public bool IsSaunaOn()
        {
            return Read(_configurationService.SaunaInputGpioPin);
        }

        public void TurnInfraredOn()
        {
            Write(_configurationService.InfraredOutputGpioPin, PinValue.Low);
        }

        public void TurnInfraredOff()
        {
            Write(_configurationService.InfraredOutputGpioPin, PinValue.High);
        }

        public bool IsInfraredOn()
        {
            return Read(_configurationService.InfraredInputGpioPin);
        }

        public async Task<int> ReadTemperature()
        {
            var temperature = 0;

            try
            {
                var sensors = await File.ReadAllLinesAsync(_configurationService.TemperatureModulesW1);

                foreach (var sensor in sensors)
                {
                    var sensorFile = String.Format(_configurationService.TemperatureModuleW1, sensor);
                    if (File.Exists(sensorFile))
                    {
                        var sensorData = await File.ReadAllLinesAsync(sensorFile);
                        var temperatureData = sensorData[1].Substring(29, sensorData[1].Length - 29);
                        temperature = Convert.ToInt32(temperatureData);
                    }
                }
            }
            catch
            {
                // Nothing we can do...
            }

            return temperature;
        }

        public void Shutdown()
        {
            if (_initialized)
            {
                _gpioController.ClosePin(_configurationService.SaunaOutputGpioPin);
                _gpioController.ClosePin(_configurationService.InfraredOutputGpioPin);
                _gpioController.ClosePin(_configurationService.SaunaInputGpioPin);
                _gpioController.ClosePin(_configurationService.InfraredInputGpioPin);

                _initialized = false;
            }
        }

        private void Write(int pinNumber, PinValue value)
        {
            if (_initialized)
            {
                _gpioController.Write(pinNumber, value);
            }
        }

        private bool Read(int pinNumber)
        {
            if (_initialized)
            {
                return _gpioController.Read(pinNumber) == PinValue.Low;
            }

            return false;
        }
    }
}