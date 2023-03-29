using MijnSauna.Frontend.Maui.Helpers;

namespace MijnSauna.Frontend.Maui.Platforms.Windows.Helpers;

public class MediaHelper : IMediaHelper
{
    private Action<MediaInfo> _callback;

    public void RegisterCallback(Action<MediaInfo> callback)
    {
        _callback = callback;
    }
}