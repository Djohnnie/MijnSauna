using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Common.Client.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Phone.Configuration;
using MijnSauna.Frontend.Phone.Factories;
using MijnSauna.Frontend.Phone.Factories.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels;

namespace MijnSauna.Frontend.Phone.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddXamarin(this IServiceCollection serviceCollection)
        {
            serviceCollection.ConfigureClient();

            serviceCollection.AddTransient<MainPage>();
            serviceCollection.AddTransient<MainPageViewModel>();
            serviceCollection.AddTransient<MainPageMasterViewModel>();
            serviceCollection.AddTransient<DetailPageViewModel>();

            serviceCollection.AddSingleton<IClientConfiguration, ClientConfiguration>();

            serviceCollection.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }
    }
}