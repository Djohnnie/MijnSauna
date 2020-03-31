using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Interfaces
{
    public interface ISmappeeSensor
    {
        Task<int> GetPowerUsage();
    }
}