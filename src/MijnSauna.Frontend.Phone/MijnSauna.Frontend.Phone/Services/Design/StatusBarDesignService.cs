using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Services.Interfaces;

namespace MijnSauna.Frontend.Phone.Services.Design
{
    public class StatusBarDesignService : IStatusBarService
    {
        public Task<bool> SetStatusBarColorFromArgb(int alpha, int red, int green, int blue)
        {
            // Nothing to do in design mode.
            return Task.FromResult(true);
        }
    }
}