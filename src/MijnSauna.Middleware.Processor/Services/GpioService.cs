using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class GpioService : IGpioService
    {
        private System.Device.Gpio.GpioController ctrl;

        public GpioService()
        {
            ctrl.OpenPin(100);
            var value = ctrl.Read(100);
            ctrl.Write(100, PinValue.High);
            ctrl.OpenPin();
        }
    }
}