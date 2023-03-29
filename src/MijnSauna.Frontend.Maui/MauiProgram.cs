using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using MijnSauna.Frontend.Maui.Factories;
using MijnSauna.Frontend.Maui.ViewModels;
using MijnSauna.Common.Client.DependencyInjection;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Maui.Configuration;
using MijnSauna.Frontend.Maui.Helpers;
using Microsoft.Maui.LifecycleEvents;
using SkiaSharp.Views.Maui.Controls.Hosting;

#if ANDROID
using Android.App;
using Android.Content;
using MijnSauna.Common.Client.Interfaces;
using AndroidMediaHelper = MijnSauna.Frontend.Maui.Platforms.Android.Helpers.MediaHelper;
using AndroidStatusBarHelper = MijnSauna.Frontend.Maui.Platforms.Android.Helpers.StatusBarHelper;
#elif WINDOWS
using WindowsMediaHelper = MijnSauna.Frontend.Maui.Platforms.Windows.Helpers.MediaHelper;
using WindowsStatusBarHelper = MijnSauna.Frontend.Maui.Platforms.Windows.Helpers.StatusBarHelper;
#endif


namespace MijnSauna.Frontend.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if ANDROID
            var mediaHelper = new AndroidMediaHelper();
            var statusBarHelper = new AndroidStatusBarHelper();
#elif WINDOWS
            var mediaHelper = new WindowsMediaHelper();
            var statusBarHelper = new WindowsStatusBarHelper();
#endif

            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnCreate(
                        (activity, _) => MyOnCreate(mediaHelper, statusBarHelper, activity)));
#elif WINDOWS
                    events.AddWindows(wndLifeCycleBuilder =>
                    {
                        wndLifeCycleBuilder.OnWindowCreated(window =>
                        {
                            IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            Microsoft.UI.WindowId win32WindowsId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                            Microsoft.UI.Windowing.AppWindow winuiAppWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(win32WindowsId);

                            //window.ExtendsContentIntoTitleBar = false;

                            //statusBarHelper.Window = winuiAppWindow;

                            if (winuiAppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                            {
                                p.Maximize();
                            }
                        });
                    });
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("mijnsauna.ttf", "MijnSaunaFont");
                });

#if ANDROID || WINDOWS
            builder.Services.AddSingleton<IMediaHelper>(mediaHelper);
            builder.Services.AddSingleton<IStatusBarHelper>(statusBarHelper);
#endif

            builder.Services.ConfigureClient();
            builder.Services.AddSingleton<IClientConfiguration, ClientConfiguration>();
            builder.Services.AddSingleton<NavigationHelper>();
            builder.Services.AddSingleton<TimerHelper>();
            builder.Services.AddSingleton<PageFactory>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SaunaPage>();

            builder.Services.AddSingleton<SaunaViewModel>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

#if ANDROID
        private static void MyOnCreate(AndroidMediaHelper mediaHelper, AndroidStatusBarHelper statusBarHelper, Activity activity)
        {
            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.android.music.metachanged");
            intentFilter.AddAction("com.android.music.playstatechanged");
            intentFilter.AddAction("com.android.music.playbackcomplete");
            intentFilter.AddAction("com.android.music.queuechanged");
            activity.RegisterReceiver(mediaHelper, intentFilter);
            
            statusBarHelper.Window = activity.Window;
        }
#endif
    }
}