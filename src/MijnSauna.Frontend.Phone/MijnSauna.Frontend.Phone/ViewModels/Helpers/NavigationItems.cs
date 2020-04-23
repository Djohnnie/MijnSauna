using System.Collections.ObjectModel;

namespace MijnSauna.Frontend.Phone.ViewModels.Helpers
{
    public class NavigationItems : ObservableCollection<NavigationItem>
    {
        public NavigationItems()
        {
            Add(new NavigationItem
            {
                Title = "Hoofdpagina",
                Type = NavigationType.Home
            });

            Add(new NavigationItem
            {
                Title = "Nieuwe sessie",
                Type = NavigationType.CreateSession
            });

            Add(new NavigationItem
            {
                Title = "Instellingen",
                Type = NavigationType.Settings
            });
        }
    }
}