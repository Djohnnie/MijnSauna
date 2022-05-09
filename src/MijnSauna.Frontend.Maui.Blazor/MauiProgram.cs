
using Microsoft.Maui.LifecycleEvents;
using MijnSauna.Common.Client.DependencyInjection;
using MijnSauna.Frontend.Maui.Blazor.Helpers;
using MijnSauna.Frontend.Maui.Blazor.Helpers.Interfaces;
using MijnSauna.Frontend.Maui.Blazor.Services;
using MijnSauna.Frontend.Maui.Blazor.ViewModels;

#if ANDROID
using Android.App;
using Android.Content;
using MijnSauna.Common.Client.Interfaces;
using AndroidClientConfiguration = MijnSauna.Frontend.Maui.Blazor.Platforms.Android.Configuration.ClientConfiguration;
using AndroidMediaService = MijnSauna.Frontend.Maui.Blazor.Platforms.Android.Services.MediaService;
#elif WINDOWS
using MijnSauna.Common.Client.Interfaces;
using WindowsClientConfiguration = MijnSauna.Frontend.Maui.Blazor.Platforms.Windows.Configuration.ClientConfiguration;
using WindowsMediaService = MijnSauna.Frontend.Maui.Blazor.Platforms.Windows.Services.MediaService;
#endif

namespace MijnSauna.Frontend.Maui.Blazor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {

#if ANDROID
        var mediaService = new AndroidMediaService();
#elif WINDOWS
        var mediaService = new WindowsMediaService();
#endif

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()

            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android.OnCreate(
                    (activity, _) => MyOnCreate(mediaService, activity)));
#endif
            })

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

#if ANDROID || WINDOWS
        builder.Services.AddSingleton<IMediaService>(mediaService);
#endif
        builder.Services.ConfigureClient();

        builder.Services.AddSingleton<ITimerHelper, TimerHelper>();

        builder.Services.AddSingleton<MainPage>();
        //builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();

#if ANDROID
        builder.Services.AddSingleton<IClientConfiguration, AndroidClientConfiguration>();
#elif WINDOWS
        builder.Services.AddSingleton<IClientConfiguration, WindowsClientConfiguration>();
#endif

        return builder.Build();
    }

#if ANDROID
    private static void MyOnCreate(AndroidMediaService mediaService, Activity activity)
    {
        var intentFilter = new IntentFilter();
        intentFilter.AddAction("com.android.music.metachanged");
        intentFilter.AddAction("com.android.music.playstatechanged");
        intentFilter.AddAction("com.android.music.playbackcomplete");
        intentFilter.AddAction("com.android.music.queuechanged");
        activity.RegisterReceiver(mediaService, intentFilter);
    }
#endif

}