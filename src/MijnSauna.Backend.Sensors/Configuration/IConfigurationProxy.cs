using System.Threading.Tasks;

namespace MijnSauna.Backend.Sensors.Configuration
{
    public interface IConfigurationProxy
    {
        Task<string> GetString(string name);

        Task<int> GetInt32(string name);
    }
}