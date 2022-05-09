using MijnSauna.Frontend.Maui.Blazor.Services;
using MijnSauna.Frontend.Maui.Blazor.Services.Models;

namespace MijnSauna.Frontend.Maui.Blazor.Platforms.Windows.Services;

public class MediaService : IMediaService
{
    private Action<MediaInfo> _callback;

    public void RegisterCallback(Action<MediaInfo> callback)
    {
        _callback = callback;
    }
}