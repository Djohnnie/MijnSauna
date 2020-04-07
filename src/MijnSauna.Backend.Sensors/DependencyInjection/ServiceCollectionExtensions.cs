using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.Sensors.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MijnSauna.Backend.Sensors.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureSensors(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISmappeeSensor, SmappeeSensor>();
            serviceCollection.AddTransient<IOpenWeatherMapSensor, OpenWeatherMapSensor>();
            serviceCollection.AddTransient<ISaunaSensor, SaunaSensor>();
        }
    }
}