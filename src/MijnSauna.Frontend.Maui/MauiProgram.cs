#if ANDROID
using Android.App;
using Android.Content;
using AndroidMediaService = MijnSauna.Frontend.Maui.Platforms.Android.Services.MediaService;
#elif WINDOWS
using WindowsMediaService = MijnSauna.Frontend.Maui.Platforms.Windows.Services.MediaService;
#endif

using Microsoft.Maui.LifecycleEvents;
using MijnSauna.Frontend.Maui.Services;
using MijnSauna.Frontend.Maui.ViewModels;

namespace MijnSauna.Frontend.Maui;

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
                fonts.AddFont("fa-brands.ttf", "FontAwesomeBrands");
                fonts.AddFont("fa-regular.ttf", "FontAwesomeRegular");
                fonts.AddFont("fa-solid.ttf", "FontAwesomeSolid");
                fonts.AddFont("mijnsauna.ttf", "MijnSaunaFont");
            });

#if ANDROID || WINDOWS
        builder.Services.AddSingleton<IMediaService>(mediaService);
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

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