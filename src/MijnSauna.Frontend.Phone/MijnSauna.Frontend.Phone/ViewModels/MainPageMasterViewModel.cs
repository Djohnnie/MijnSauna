using MijnSauna.Frontend.Phone.ViewModels.Base;
using MijnSauna.Frontend.Phone.ViewModels.Helpers;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageMasterViewModel : ViewModelBase
    {
        public NavigationItems NavigationItems { get; } = new NavigationItems();
    }
}