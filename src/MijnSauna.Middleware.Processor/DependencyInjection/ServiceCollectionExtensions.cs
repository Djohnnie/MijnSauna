using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Common.Client.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Middleware.Processor.Configuration;
using MijnSauna.Middleware.Processor.Context;
using MijnSauna.Middleware.Processor.Context.Interfaces;
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
            serviceCollection.ConfigureClient();

            serviceCollection.AddSingleton<ISessionContext, SessionContext>();

            serviceCollection.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));

            serviceCollection.AddSingleton<IClientConfiguration, ClientConfiguration>();

            serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();
            serviceCollection.AddSingleton<ISessionService, SessionService>();
            serviceCollection.AddSingleton<IGpioService, GpioService>();
            serviceCollection.AddSingleton<ILogService, LogService>();

            serviceCollection.AddSingleton<IGpioController, GpioController>();
        }
    }
}