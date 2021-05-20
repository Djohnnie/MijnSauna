using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Interfaces
{
    public interface IShellySensor
    {
        Task<(decimal, decimal)> GetPowerUsage();
    }
}