using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Interfaces
{
    public interface ISaunaSensor
    {
        Task<int> GetTemperature();

        Task<(bool, bool)> GetState();
    }
}