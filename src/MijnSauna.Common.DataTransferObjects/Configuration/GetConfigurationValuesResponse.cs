using System.Collections.Generic;

namespace MijnSauna.Common.DataTransferObjects.Configuration
{
    public class GetConfigurationValuesResponse
    {
        public List<ConfigurationValue> ConfigurationValues { get; set; }
    }
}