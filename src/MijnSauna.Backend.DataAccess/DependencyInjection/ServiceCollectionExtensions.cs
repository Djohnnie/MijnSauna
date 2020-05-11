using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.DataAccess.Repositories;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccess(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<ConfigurationValue>, ConfigurationValueRepository>();
            serviceCollection.AddTransient<IRepository<Session>, SessionRepository>();
            serviceCollection.AddTransient<IRepository<Sample>, SampleRepository>();
            serviceCollection.AddTransient<IRepository<Log>, LogRepository>();
            serviceCollection.AddDbContext<DatabaseContext>();
        }
    }
}