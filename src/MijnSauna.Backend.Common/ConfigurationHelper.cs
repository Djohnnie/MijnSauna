using System;
using MijnSauna.Backend.Common.Interfaces;

namespace MijnSauna.Backend.Common
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ConnectionString { get; set; }
        public string ClientId { get; set; }
    }
}