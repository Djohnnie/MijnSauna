using System;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Sensors.Configuration;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic
{
    public class ConfigurationProxy : IConfigurationProxy
    {
        private readonly IConfigurationLogic _configurationLogic;

        public ConfigurationProxy(
            IConfigurationLogic configurationLogic)
        {
            _configurationLogic = configurationLogic;
        }

        public async Task<string> GetString(string name)
        {
            var value = await _configurationLogic.GetConfigurationValue(name);
            return value.Value;
        }

        public async Task<int> GetInt32(string name)
        {
            var value = await _configurationLogic.GetConfigurationValue(name);
            return Convert.ToInt32(value.Value);
        }
    }
}