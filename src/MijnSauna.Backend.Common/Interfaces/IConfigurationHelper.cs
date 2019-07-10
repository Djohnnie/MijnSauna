using System;

namespace MijnSauna.Backend.Common.Interfaces
{
    public interface IConfigurationHelper
    {
        Guid Id { get; set; }
        String ConnectionString { get; set; }
    }
}