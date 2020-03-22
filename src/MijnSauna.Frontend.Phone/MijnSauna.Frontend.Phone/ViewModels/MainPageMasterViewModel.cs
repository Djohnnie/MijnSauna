using MijnSauna.Frontend.Phone.ViewModels.Base;
using MijnSauna.Frontend.Phone.ViewModels.Events;
using MijnSauna.Frontend.Phone.ViewModels.Helpers;
using Reactive.EventAggregator;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageMasterViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        public NavigationItems NavigationItems { get; } = new NavigationItems();

        private NavigationItem _selectedNavigationItem;

        public NavigationItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set
            {
                _selectedNavigationItem = value;
                OnPropertyChanged(nameof(SelectedNavigationItem));

                if (value != null)
                {
                    _eventAggregator.Publish(new NavigationItemSelected
                    {
                        Type = value.Type
                    });
                }
            }
        }

        public MainPageMasterViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}