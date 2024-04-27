using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Interfaces;

public interface ISolarSensor
{
    Task<int> GetBatteryPercentage();
}