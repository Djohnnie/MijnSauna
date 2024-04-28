using Android.Content;
using MijnSauna.Frontend.Maui.Helpers;

namespace MijnSauna.Frontend.Maui.Platforms.Android.Helpers;

[BroadcastReceiver(Enabled = true, Exported = true)]
public class MediaHelper : BroadcastReceiver, IMediaHelper
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