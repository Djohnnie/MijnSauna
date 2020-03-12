using MijnSauna.Frontend.Phone.Services.Interfaces;
using System.Threading.Tasks;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageViewModel
    {
        private readonly IStatusBarService _statusBarService;

        public MainPageViewModel(IStatusBarService statusBarService)
        {
            _statusBarService = statusBarService;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(2500);
                    _statusBarService.SetStatusBarColorFromArgb(255, 255, 0, 0);
                    await Task.Delay(2500);
                    _statusBarService.SetStatusBarColorFromArgb(255, 0, 255, 0);
                    await Task.Delay(2500);
                    _statusBarService.SetStatusBarColorFromArgb(255, 0, 0, 255);
                }
            });
        }
    }
}