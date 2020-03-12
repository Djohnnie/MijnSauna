using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone
{
    public partial class App : Application
    {

        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}