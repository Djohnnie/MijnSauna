using Android.Views;
using MijnSauna.Frontend.Phone.Services.Interfaces;

namespace MijnSauna.Frontend.Phone.Droid.Services
{
    public class SoundService : ISoundService
    {
        private readonly MainActivity _activity;

        public SoundService(MainActivity activity)
        {
            _activity = activity;
        }

        public void PlayClickSound()
        {
            
            var view = _activity.FindViewById<View>(Android.Resource.Id.Content);
            view.PlaySoundEffect(SoundEffects.Click);
        }
    }
}