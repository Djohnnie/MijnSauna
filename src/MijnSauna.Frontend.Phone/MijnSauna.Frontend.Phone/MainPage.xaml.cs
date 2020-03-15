using MijnSauna.Frontend.Phone.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm, IStatusBarService statusBarService)
        {
            InitializeComponent();
            BindingContext = vm;
            
            vm.PropertyChanged += async (o, args) =>
            {
                if (args.PropertyName == nameof(vm.SessionState))
                {
                    await Task.Delay(10);
                    statusBarService.SetStatusBarColorFromArgb(
                        (int)(BackgroundColor.A * 255),
                        (int)(BackgroundColor.R * 255),
                        (int)(BackgroundColor.G * 255),
                        (int)(BackgroundColor.B * 255));
                }
            };
        }
    }
}