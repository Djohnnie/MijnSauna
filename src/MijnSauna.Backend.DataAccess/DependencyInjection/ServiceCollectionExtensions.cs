using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Backend.DataAccess.Repositories;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccess(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<Session>, SessionRepository>();
            serviceCollection.AddTransient<IRepository<Sample>, SampleRepository>();
            serviceCollection.AddDbContext<DatabaseContext>();
        }
    }
}