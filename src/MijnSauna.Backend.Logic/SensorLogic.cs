using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Sensors.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sensor;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic
{
    public class SensorLogic : ISensorLogic
    {
        private readonly ISmappeeSensor _smappeeSensor;
        private readonly IShellySensor _shellySensor;
        private readonly ISaunaSensor _saunaSensor;
        private readonly IOpenWeatherMapSensor _openWeatherMapSensor;
        private readonly ISolarSensor _solarSensor;

        public SensorLogic(
            ISmappeeSensor smappeeSensor,
            IShellySensor shellySensor,
            ISaunaSensor saunaSensor,
            IOpenWeatherMapSensor openWeatherMapSensor,
            ISolarSensor solarSensor)
        {
            _smappeeSensor = smappeeSensor;
            _shellySensor = shellySensor;
            _saunaSensor = saunaSensor;
            _openWeatherMapSensor = openWeatherMapSensor;
            _solarSensor = solarSensor;
        }

        public async Task<GetPowerUsageResponse> GetPowerUsage()
        {
            var result = await _smappeeSensor.GetPowerUsage();
            var battery = await _solarSensor.GetBatteryPercentage();

            return new GetPowerUsageResponse
            {
                PowerUsage = result / 1000M,
                BatteryPercentage = battery
            };
        }

        public async Task<GetSaunaPowerUsageResponse> GetSaunaPowerUsage()
        {
            var result = await _shellySensor.GetPowerUsage();
            var battery = await _solarSensor.GetBatteryPercentage();

            return new GetSaunaPowerUsageResponse
            {
                SaunaPowerUsage = result.Item1 / 1000M,
                InfraredPowerUsage = result.Item2 / 1000M,
                BatteryPercentage = battery
            };
        }

        public async Task<GetSaunaTemperatureResponse> GetSaunaTemperature()
        {
            var result = await _saunaSensor.GetTemperature();

            return new GetSaunaTemperatureResponse
            {
                Temperature = result
            };
        }

        public async Task<GetOutsideTemperatureResponse> GetOutsideTemperature()
        {
            var result = await _openWeatherMapSensor.GetOutsideTemperature();

            return new GetOutsideTemperatureResponse
            {
                Temperature = result
            };
        }

        public async Task<GetSaunaStateResponse> GetSaunaState()
        {
            var (isSaunaOn, isInfraredOn) = await _saunaSensor.GetState();

            return new GetSaunaStateResponse
            {
                IsSaunaOn = isSaunaOn,
                IsInfraredOn = isInfraredOn
            };
        }
    }
}