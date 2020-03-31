using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Configuration
{
    public interface IConfigurationProxy
    {
        Task<string> GetValue(string name);
    }
}