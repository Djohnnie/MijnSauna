using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Frontend.Maui.Blazor;

public partial class App : Application
{
    public App(
        IClientConfiguration clientConfiguration,
        MainPage mainPage, SettingsPage settingsPage)
    {
        InitializeComponent();

        if (string.IsNullOrWhiteSpace(clientConfiguration.ServiceBaseUrl) || string.IsNullOrWhiteSpace(clientConfiguration.ClientId))
        {
            MainPage = settingsPage;
        }
        else
        {
            MainPage = mainPage;
        }
    }
}