using System;

namespace MijnSauna.Backend.Common.Interfaces
{
    public interface IConfigurationHelper
    {
        Guid Id { get; set; }
        string ConnectionString { get; set; }
        string ClientId { get; set; }
    }
}