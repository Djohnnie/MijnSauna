using MijnSauna.Frontend.Maui.Services;
using MijnSauna.Frontend.Maui.Services.Models;

namespace MijnSauna.Frontend.Maui.Platforms.Windows.Services;

public class MediaService : IMediaService
{
    private Action<MediaInfo> _callback;

    public void RegisterCallback(Action<MediaInfo> callback)
    {
        _callback = callback;
    }
}