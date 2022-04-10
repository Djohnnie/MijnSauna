using MijnSauna.Frontend.Maui.Services.Models;

namespace MijnSauna.Frontend.Maui.Services;

public interface IMediaService
{
    void RegisterCallback(Action<MediaInfo> callback);
}