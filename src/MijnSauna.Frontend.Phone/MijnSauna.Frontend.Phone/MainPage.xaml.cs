using MijnSauna.Frontend.Phone.Services.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnSauna.Frontend.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage(MainPageViewModel vm, IStatusBarService statusBarService)
        {
            InitializeComponent();
            BindingContext = vm;

            vm.PropertyChanged += async (o, args) =>
            {
                if (args.PropertyName == nameof(vm.SessionState))
                {
                    await statusBarService.SetStatusBarColorFromArgb(
                        (int)(BackgroundColor.A * 255),
                        (int)(BackgroundColor.R * 255),
                        (int)(BackgroundColor.G * 255),
                        (int)(BackgroundColor.B * 255));
                }
            };
        }

        private async void MainPage_OnAppearing(object sender, EventArgs e)
        {
            if (BindingContext is MainPageViewModel vm)
            {
                await vm.OnAppearing();
            }
        }
    }
}