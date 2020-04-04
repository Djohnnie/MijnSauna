using MijnSauna.Common.DataTransferObjects.Sensor;
using System.Threading.Tasks;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface ISensorClient
    {
        Task<GetPowerUsageResponse> GetPowerUsage();

        Task<GetSaunaTemperatureResponse> GetSaunaTemperature();

        Task<GetOutsideTemperatureResponse> GetOutsideTemperature();
    }
}