using MijnSauna.Frontend.Maui.Blazor.Services.Models;

namespace MijnSauna.Frontend.Maui.Blazor.Services;

public interface IMediaService
{
    void RegisterCallback(Action<MediaInfo> callback);
}