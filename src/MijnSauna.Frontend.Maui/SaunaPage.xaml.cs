using MijnSauna.Frontend.Maui.ViewModels;

namespace MijnSauna.Frontend.Maui;

public partial class SaunaPage : ContentPage
{
    public SaunaPage(SaunaViewModel vm)
    {
        InitializeComponent();

        SizeChanged += SaunaPage_SizeChanged;

        BindingContext = vm;
    }

    private void SaunaPage_SizeChanged(object sender, EventArgs e)
    {
        var vm = BindingContext as SaunaViewModel;
        if (vm != null)
        {
            vm.PageHeight = (int)Height;
            vm.PageWidth = (int)Width;
        }
    }
}