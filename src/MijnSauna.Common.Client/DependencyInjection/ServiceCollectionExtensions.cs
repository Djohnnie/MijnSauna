using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MijnSauna.Common.Client.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigurationClient, ConfigurationClient>();
            serviceCollection.AddTransient<ISessionClient, SessionClient>();
            serviceCollection.AddTransient<ISampleClient, SampleClient>();
            serviceCollection.AddTransient<ISensorClient, SensorClient>();
            serviceCollection.AddTransient<ILogClient, LogClient>();

            serviceCollection.AddSingleton<IServiceClient, ServiceClient>();
        }
    }
}