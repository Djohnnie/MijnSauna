using MijnSauna.Frontend.Phone.Services.Interfaces;

namespace MijnSauna.Frontend.Phone.Services.Design
{
    public class StatusBarDesignService : IStatusBarService
    {
        public void SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            // Nothing to do in design mode.
        }
    }
}