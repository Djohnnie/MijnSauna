using System;
using Android.Content;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using MediaInfo = MijnSauna.Frontend.Phone.Services.Models.MediaInfo;

namespace MijnSauna.Frontend.Phone.Droid.Services
{
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
}