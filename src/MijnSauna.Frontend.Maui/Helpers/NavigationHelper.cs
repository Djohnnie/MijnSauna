using System.Diagnostics;

namespace MijnSauna.Frontend.Maui.Helpers;

public class NavigationHelper
{
    readonly IServiceProvider _services;

    protected INavigation Navigation
    {
        get
        {
            INavigation? navigation = Application.Current?.MainPage?.Navigation;
            if (navigation is not null)
                return navigation;
            else
            {
                //This is not good!
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw new Exception();
            }
        }
    }
    public NavigationHelper(IServiceProvider services)
    {
        _services = services;
    }

    public Task NavigateToMainPage()
    {
        var page = new NavigationPage(_services.GetService<MainPage>());
        if (page is not null)
        {
            return Navigation.PushAsync(page, true);
        }

        return Navigation.PopAsync();
    }
}