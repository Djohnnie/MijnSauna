using MijnSauna.Common.DataTransferObjects.Sensor;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic.Interfaces
{
    public interface ISensorLogic : ILogic
    {
        Task<GetPowerUsageResponse> GetPowerUsage();

        Task<GetSaunaTemperatureResponse> GetSaunaTemperature();

        Task<GetOutsideTemperatureResponse> GetOutsideTemperature();

        Task<GetSaunaStateResponse> GetSaunaState();
    }
}