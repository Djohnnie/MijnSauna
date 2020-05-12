using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Services.Interfaces;

namespace MijnSauna.Frontend.Phone.Services.Design
{
    public class StatusBarDesignService : IStatusBarService
    {
        public Task SetFullscreen(bool fullscreen)
        {
            // Nothing to do in design mode.
            return Task.CompletedTask;
        }

        public Task KeepScreenOn(bool keepScreenOn)
        {
            // Nothing to do in design mode.
            return Task.CompletedTask;
        }

        public Task SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            // Nothing to do in design mode.
            return Task.CompletedTask;
        }
    }
}