using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Sensors.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sensor;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic
{
    public class SensorLogic : ISensorLogic
    {
        private readonly ISmappeeSensor _smappeeSensor;
        private readonly ISaunaSensor _saunaSensor;
        private readonly IOpenWeatherMapSensor _openWeatherMapSensor;

        public SensorLogic(
            ISmappeeSensor smappeeSensor,
            ISaunaSensor saunaSensor,
            IOpenWeatherMapSensor openWeatherMapSensor)
        {
            _smappeeSensor = smappeeSensor;
            _saunaSensor = saunaSensor;
            _openWeatherMapSensor = openWeatherMapSensor;
        }

        public async Task<GetPowerUsageResponse> GetPowerUsage()
        {
            var result = await _smappeeSensor.GetPowerUsage();
            return new GetPowerUsageResponse
            {
                PowerUsage = result
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