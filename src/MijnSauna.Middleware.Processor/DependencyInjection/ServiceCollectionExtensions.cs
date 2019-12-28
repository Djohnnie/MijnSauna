using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Middleware.Processor.Services;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureProcessor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISessionService, SessionService>();
        }
    }
}