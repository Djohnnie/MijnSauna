using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Sensors.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sensor;
using System;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic
{
    public class SensorLogic : ISensorLogic
    {
        private readonly IConfigurationLogic _configurationLogic;
        private readonly ISmappeeSensor _smappeeSensor;
        private readonly IOpenWeatherMapSensor _openWeatherMapSensor;

        public SensorLogic(
            IConfigurationLogic configurationLogic,
            ISmappeeSensor smappeeSensor,
            IOpenWeatherMapSensor openWeatherMapSensor)
        {
            _configurationLogic = configurationLogic;
            _smappeeSensor = smappeeSensor;
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
            throw new NotImplementedException();
        }

        public async Task<GetOutsideTemperatureResponse> GetOutsideTemperature()
        {
            var result = await _openWeatherMapSensor.GetOutsideTemperature();
            return new GetOutsideTemperatureResponse
            {
                Temperature = result
            };
        }
    }
}