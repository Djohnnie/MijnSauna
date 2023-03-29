using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Maui.Factories;

namespace MijnSauna.Frontend.Maui
{
    public partial class App : Application
    {
        private readonly PageFactory _pageFactory;

        public App(
            IClientConfiguration clientConfiguration,
            PageFactory pageFactory)
        {
            _pageFactory = pageFactory;

            InitializeComponent();
            
            if (clientConfiguration.IsSaunaMode)
            {
                MainPage = NavigationPage<SaunaPage>();
            }
            else
            {
                MainPage = NavigationPage<MainPage>();
            }
        }

        private Page NavigationPage<TPage>() where TPage : ContentPage
        {
            return new NavigationPage(_pageFactory.CreatePage<TPage>());
        }
    }
}