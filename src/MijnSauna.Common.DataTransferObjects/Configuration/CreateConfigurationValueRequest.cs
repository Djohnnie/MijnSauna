using System;

namespace MijnSauna.Common.DataTransferObjects.Configuration
{
    public class CreateConfigurationValueRequest
    {
        public String Name { get; set; }
        public String Value { get; set; }
    }
}