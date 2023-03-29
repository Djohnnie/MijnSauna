using Microsoft.UI.Windowing;
using MijnSauna.Frontend.Maui.Helpers;

namespace MijnSauna.Frontend.Maui.Platforms.Windows.Helpers;

public class StatusBarHelper : IStatusBarHelper
{
    public AppWindow Window { get; set; }

    public Task KeepScreenOn(bool keepScreenOn)
    {
        return Task.CompletedTask;
    }

    public Task SetFullscreen(bool fullscreen)
    {
        return Task.CompletedTask;
    }

    public Task SetStatusBarColorFromRgb(byte red, byte green, byte blue)
    {
        if (Window != null)
        {
            var color = new global::Windows.UI.Color() { R = red, G = green, B = blue };
            Window.TitleBar.ButtonBackgroundColor = color;
            Window.TitleBar.ButtonHoverBackgroundColor = color;
            Window.TitleBar.ButtonHoverForegroundColor = new global::Windows.UI.Color() { R = 255, G = 255, B = 255 }; ;
            Window.TitleBar.ForegroundColor = new global::Windows.UI.Color() { R = 255, G = 255, B = 255 };
            Window.TitleBar.BackgroundColor = color;
        }

        return Task.CompletedTask;
    }
}