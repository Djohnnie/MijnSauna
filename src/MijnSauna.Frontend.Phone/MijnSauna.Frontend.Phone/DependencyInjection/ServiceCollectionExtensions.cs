using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Frontend.Phone.ViewModels;

namespace MijnSauna.Frontend.Phone.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddXamarin(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MainPage>();
            serviceCollection.AddTransient<MainPageViewModel>();
        }
    }
}