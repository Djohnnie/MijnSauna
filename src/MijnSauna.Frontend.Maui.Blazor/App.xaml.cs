namespace MijnSauna.Frontend.Maui.Blazor;

public partial class App : Application
{
    public App(MainPage mainPage)
    {
        InitializeComponent();

        MainPage = mainPage;
    }
}