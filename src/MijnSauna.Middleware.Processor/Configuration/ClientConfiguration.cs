using Microsoft.Extensions.Configuration;
using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Middleware.Processor.Configuration
{
    public class ClientConfiguration : IClientConfiguration
    {
        public string ServiceBaseUrl { get; set; }
        public string ClientId { get; set; }
        public bool IsSaunaMode { get; set; }

        public ClientConfiguration(IConfiguration configuration)
        {
            ServiceBaseUrl = configuration["ServiceBaseUrl"];
            ClientId = configuration["ClientId"];
        }
    }
}