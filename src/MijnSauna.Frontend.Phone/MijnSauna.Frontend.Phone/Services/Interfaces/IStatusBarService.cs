using System.Threading.Tasks;

namespace MijnSauna.Frontend.Phone.Services.Interfaces
{
    public interface IStatusBarService
    {
        Task SetFullscreen(bool fullscreen);

        Task KeepScreenOn(bool keepScreenOn);

        Task SetStatusBarColorFromArgb(int alpha, int red, int green, int blue);
    }
}