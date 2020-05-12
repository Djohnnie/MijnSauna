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
        }

        public async Task SetFullscreen(bool fullscreen)
        {
            await BeginInvokeOnMainThreadAsync(() =>
            {
                if (fullscreen)
                {
                    _window.AddFlags(WindowManagerFlags.Fullscreen);
                }
                else
                {
                    _window.ClearFlags(WindowManagerFlags.Fullscreen);
                }
            });
        }

        public async Task KeepScreenOn(bool keepScreenOn)
        {
            await BeginInvokeOnMainThreadAsync(() =>
            {
                if (keepScreenOn)
                {
                    _window.AddFlags(WindowManagerFlags.KeepScreenOn);
                }
                else
                {
                    _window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                }
            });
        }

        public async Task SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            await BeginInvokeOnMainThreadAsync(() =>
            {
                _window.SetStatusBarColor(Color.Argb(alpha, red, green, blue));
            });
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