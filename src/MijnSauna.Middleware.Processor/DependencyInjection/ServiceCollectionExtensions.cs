using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Middleware.Processor.Controllers;
using MijnSauna.Middleware.Processor.Controllers.Interfaces;
using MijnSauna.Middleware.Processor.Services;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureProcessor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();
            serviceCollection.AddSingleton<ISessionService, SessionService>();
            serviceCollection.AddSingleton<IBackendService, BackendService>();
            serviceCollection.AddSingleton<IGpioService, GpioService>();
            serviceCollection.AddSingleton<IGpioController, GpioController>();
        }
    }
}