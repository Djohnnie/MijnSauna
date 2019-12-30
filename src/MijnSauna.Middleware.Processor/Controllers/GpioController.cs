using System.Device.Gpio;
using MijnSauna.Middleware.Processor.Controllers.Interfaces;

namespace MijnSauna.Middleware.Processor.Controllers
{
    public class GpioController : IGpioController
    {
        private readonly System.Device.Gpio.GpioController _gpioController = new System.Device.Gpio.GpioController();
        //private readonly System.Device.Gpio.GpioController _gpioController;

        public void OpenPin(int pinNumber, PinMode mode)
        {
            _gpioController.OpenPin(pinNumber, mode);
        }

        public PinValue Read(int pinNumber)
        {
            return _gpioController.Read(pinNumber);
        }

        public void Write(int pinNumber, PinValue value)
        {
            _gpioController.Write(pinNumber, value);
        }

        public void ClosePin(int pinNumber)
        {
            _gpioController.ClosePin(pinNumber);
        }
    }
}