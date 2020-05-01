using System;
using Microsoft.Extensions.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone
{
    public partial class App : Application
    {
        public App(
            IClientConfiguration clientConfiguration,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            if (clientConfiguration.IsSaunaMode)
            {
                MainPage = serviceProvider.GetService<SaunaPage>();
            }
            else
            {
                MainPage = serviceProvider.GetService<MainPage>();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}