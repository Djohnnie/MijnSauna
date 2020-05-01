using MijnSauna.Frontend.Phone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnSauna.Frontend.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaunaPage : ContentPage
    {
        public SaunaPage(SaunaPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}