using Android.Views;
using MijnSauna.Frontend.Maui.Helpers;
using Window = Android.Views.Window;
using Color = Android.Graphics.Color;

namespace MijnSauna.Frontend.Maui.Platforms.Android.Helpers;

public class StatusBarHelper : IStatusBarHelper
{
    public Window Window { get; set; }

    public StatusBarHelper()
    {
    }

    public async Task SetFullscreen(bool fullscreen)
    {
        await BeginInvokeOnMainThreadAsync(() =>
        {
            if (fullscreen)
            {
                Window.AddFlags(WindowManagerFlags.Fullscreen);
            }
            else
            {
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }
        });
    }

    public async Task KeepScreenOn(bool keepScreenOn)
    {
        await BeginInvokeOnMainThreadAsync(() =>
        {
            if (keepScreenOn)
            {
                Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            }
            else
            {
                Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
        });
    }

    public async Task SetStatusBarColorFromRgb(byte red, byte green, byte blue)
    {
        await BeginInvokeOnMainThreadAsync(() =>
        {
            Window.SetStatusBarColor(Color.Rgb(red, green, blue));
        });
    }

    public Task BeginInvokeOnMainThreadAsync(Action action)
    {
        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
        
        Device.BeginInvokeOnMainThread(() =>
        {
            try
            {
                action();
                tcs.SetResult(null);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }
}