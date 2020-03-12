using Android.Graphics;
using Android.Views;
using MijnSauna.Frontend.Phone.Services.Interfaces;

namespace MijnSauna.Frontend.Phone.Droid.Services
{
    public class StatusBarService : IStatusBarService
    {
        private readonly Window _window;

        public StatusBarService(Window window)
        {
            _window = window;
        }

        public void SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            _window.SetStatusBarColor(Color.Argb(alpha, red, green, blue));
        }
    }
}