using MijnSauna.Frontend.Maui.ViewModels;

namespace MijnSauna.Frontend.Maui;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}