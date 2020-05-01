using System;
using System.Threading.Tasks;
using Android.Views;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

namespace MijnSauna.Frontend.Phone.Droid.Services
{
    public class StatusBarService : IStatusBarService
    {
        private readonly Window _window;

        public StatusBarService(Window window)
        {
            _window = window;
            window.SetFlags(
                WindowManagerFlags.KeepScreenOn | WindowManagerFlags.Fullscreen,
                WindowManagerFlags.KeepScreenOn | WindowManagerFlags.Fullscreen);
        }

        public async Task<bool> SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            await BeginInvokeOnMainThreadAsync(() =>
            {
                _window.SetStatusBarColor(Color.Argb(alpha, red, green, blue));
            });

            return true;
        }

        public Task BeginInvokeOnMainThreadAsync(Action action)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }
    }
}