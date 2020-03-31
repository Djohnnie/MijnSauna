using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Interfaces
{
    public interface IOpenWeatherMapSensor
    {
        Task<int> GetOutsideTemperature();
    }
}