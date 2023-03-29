namespace MijnSauna.Frontend.Maui.Helpers;

public interface IStatusBarHelper
{
    Task SetFullscreen(bool fullscreen);

    Task KeepScreenOn(bool keepScreenOn);

    Task SetStatusBarColorFromRgb(byte red, byte green, byte blue);
}