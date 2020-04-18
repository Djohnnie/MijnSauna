using System;
using System.Device.Gpio;
using MijnSauna.Middleware.Processor.Controllers.Interfaces;
using GpioSystemController = System.Device.Gpio.GpioController;

namespace MijnSauna.Middleware.Processor.Controllers
{
    public class GpioController : IGpioController
    {
        private readonly GpioSystemController _gpioController;

        private bool _isSupported;

        public GpioController()
        {
            try
            {
                _gpioController = new GpioSystemController();
                _isSupported = true;
            }
            catch (NotSupportedException)
            {
                _isSupported = false;
            }
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            if (_isSupported)
            {
                _gpioController.OpenPin(pinNumber, mode);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public PinValue Read(int pinNumber)
        {
            if (_isSupported)
            {
                return _gpioController.Read(pinNumber);
            }

            throw new NotSupportedException();
        }

        public void Write(int pinNumber, PinValue value)
        {
            if (_isSupported)
            {
                _gpioController.Write(pinNumber, value);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void ClosePin(int pinNumber)
        {
            if (_isSupported)
            {
                _gpioController.ClosePin(pinNumber);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}