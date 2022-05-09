using MijnSauna.Frontend.Maui.Blazor.ViewModels;

namespace MijnSauna.Frontend.Maui.Blazor;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}