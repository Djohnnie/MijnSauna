using MijnSauna.Frontend.Maui.ViewModels;

namespace MijnSauna.Frontend.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}