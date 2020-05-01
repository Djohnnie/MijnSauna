using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Common.Client.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Phone.Configuration;
using MijnSauna.Frontend.Phone.Factories;
using MijnSauna.Frontend.Phone.Factories.Interfaces;
using MijnSauna.Frontend.Phone.Helpers;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels;
using Reactive.EventAggregator;

namespace MijnSauna.Frontend.Phone.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddXamarin(this IServiceCollection serviceCollection)
        {
            serviceCollection.ConfigureClient();

            serviceCollection.AddTransient<MainPage>();
            serviceCollection.AddTransient<SaunaPage>();
            serviceCollection.AddTransient<MainPageViewModel>();
            serviceCollection.AddTransient<SaunaPageViewModel>();
            serviceCollection.AddTransient<MainPageMasterViewModel>();
            serviceCollection.AddTransient<DetailPageViewModel>();
            serviceCollection.AddTransient<HomeViewModel>();
            serviceCollection.AddTransient<CreateSessionViewModel>();
            serviceCollection.AddTransient<SettingsViewModel>();

            serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();

            serviceCollection.AddSingleton<IClientConfiguration, ClientConfiguration>();

            serviceCollection.AddSingleton<IViewModelFactory, ViewModelFactory>();

            serviceCollection.AddTransient<ITimerHelper, TimerHelper>();
        }
    }
}