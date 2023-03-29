namespace MijnSauna.Frontend.Maui.Helpers;

public interface IMediaHelper
{
    void RegisterCallback(Action<MediaInfo> callback);
}

public class MediaInfo
{
    public string Artist { get; set; }
    public string Track { get; set; }
    public string Album { get; set; }
}