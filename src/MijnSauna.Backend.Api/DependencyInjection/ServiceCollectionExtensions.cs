using System;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.Common.DependencyInjection;
using MijnSauna.Backend.Common.Interfaces;
using MijnSauna.Backend.Logic.DependencyInjection;

namespace MijnSauna.Backend.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApi(this IServiceCollection serviceCollection, Action<IConfigurationHelper> configuration)
        {
            serviceCollection.ConfigureCommon(configuration);
            serviceCollection.ConfigureLogic();
        }
    }
}