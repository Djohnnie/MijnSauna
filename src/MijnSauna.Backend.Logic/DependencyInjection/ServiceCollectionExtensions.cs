using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.DataAccess.DependencyInjection;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Logic.Validation;
using MijnSauna.Backend.Logic.Validation.Interfaces;
using MijnSauna.Backend.Mappers.DependencyInjection;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Logic.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.ConfigureDataAccess();
            serviceCollection.ConfigureMappers();
            serviceCollection.AddTransient<IConfigurationLogic, ConfigurationLogic>();
            serviceCollection.AddTransient<ISessionLogic, SessionLogic>();
            serviceCollection.AddTransient<ISampleLogic, SampleLogic>();
            serviceCollection.AddSingleton<IValidator<CreateSessionRequest>, CreateSessionRequestValidator>();
        }
    }
}