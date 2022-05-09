using Android.Content;
using MijnSauna.Frontend.Maui.Blazor.Services;
using MijnSauna.Frontend.Maui.Blazor.Services.Models;

namespace MijnSauna.Frontend.Maui.Blazor.Platforms.Android.Services;

public class MediaService : BroadcastReceiver, IMediaService
{
    private Action<MediaInfo> _callback;

    public override void OnReceive(Context context, Intent intent)
    {
        if (_callback != null)
        {
            var playing = intent.GetBooleanExtra("playing", false);
            var artist = intent.GetStringExtra("artist");
            var album = intent.GetStringExtra("album");
            var track = intent.GetStringExtra("track");

            _callback(playing ? new MediaInfo
            {
                Artist = artist,
                Track = track,
                Album = album
            } : null);
        }
    }

    public void RegisterCallback(Action<MediaInfo> callback)
    {
        _callback = callback;
    }
}