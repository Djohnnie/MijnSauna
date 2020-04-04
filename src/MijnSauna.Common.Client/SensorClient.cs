using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sensor;
using System.Threading.Tasks;

namespace MijnSauna.Common.Client
{
    public class SensorClient : ISensorClient
    {
        private readonly IServiceClient _serviceClient;

        public SensorClient(
            IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Task<GetPowerUsageResponse> GetPowerUsage()
        {
            return _serviceClient.Get<GetPowerUsageResponse>("sensors/power");
        }

        public Task<GetOutsideTemperatureResponse> GetOutsideTemperature()
        {
            return _serviceClient.Get<GetOutsideTemperatureResponse>("sensors/temperature/outside");
        }

        public Task<GetSaunaTemperatureResponse> GetSaunaTemperature()
        {
            return _serviceClient.Get<GetSaunaTemperatureResponse>("sensors/temperature/sauna");
        }
    }
}