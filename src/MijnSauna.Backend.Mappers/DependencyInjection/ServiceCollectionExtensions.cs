using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Configuration;
using MijnSauna.Common.DataTransferObjects.Logs;
using MijnSauna.Common.DataTransferObjects.Samples;
using MijnSauna.Common.DataTransferObjects.Sessions;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;

namespace MijnSauna.Backend.Mappers.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper<IList<ConfigurationValue>, GetConfigurationValuesResponse>, GetConfigurationValuesResponseMapper>();
            serviceCollection.AddSingleton<IMapper<ConfigurationValue, GetConfigurationValueResponse>, GetConfigurationValueResponseMapper>();
            serviceCollection.AddSingleton<IMapper<CreateConfigurationValueRequest, ConfigurationValue>, CreateConfigurationValueRequestMapper>();
            serviceCollection.AddSingleton<IMapper<ConfigurationValue, CreateConfigurationValueResponse>, CreateConfigurationValueResponseMapper>();

            serviceCollection.AddSingleton<IMapper<Session, GetActiveSessionResponse>, GetActiveSessionResponseMapper>();

            serviceCollection.AddSingleton<IMapper<Session, CreateSessionRequest>, CreateSessionRequestMapper>();
            serviceCollection.AddSingleton<IMapper<Session, CreateSessionResponse>, CreateSessionResponseMapper>();

            serviceCollection.AddSingleton<IMapper<IList<Sample>, GetSamplesForSessionResponse>, GetSamplesForSessionResponseMapper>();
            serviceCollection.AddSingleton<IMapper<Sample, CreateSampleForSessionRequest>, CreateSampleForSessionRequestMapper>();
            serviceCollection.AddSingleton<IMapper<Sample, CreateSampleForSessionResponse>, CreateSampleForSessionResponseMapper>();

            serviceCollection.AddSingleton<IMapper<Log, LogInformationRequest>, LogInformationRequestMapper>();
            serviceCollection.AddSingleton<IMapper<Log, LogErrorRequest>, LogErrorRequestMapper>();
        }
    }
}