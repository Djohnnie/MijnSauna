using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Frontend.Maui;

public partial class App : Application
{
    public App(
        IClientConfiguration clientConfiguration,
        MainPage mainPage, SettingsPage settingsPage)
    {
        InitializeComponent();

        LiveCharts.Configure(config =>
            config
                // registers SkiaSharp as the library backend
                // REQUIRED unless you build your own
                .AddSkiaSharp()

                // adds the default supported types
                // OPTIONAL but highly recommend
                .AddDefaultMappers()

                // select a theme, default is Light
                // OPTIONAL
                //.AddDarkTheme()
                .AddLightTheme()
        );

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