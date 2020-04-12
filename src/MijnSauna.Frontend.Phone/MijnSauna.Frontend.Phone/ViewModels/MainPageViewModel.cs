using System;
using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.Factories.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels.Base;
using MijnSauna.Frontend.Phone.ViewModels.Events;
using MijnSauna.Frontend.Phone.ViewModels.Helpers;
using Reactive.EventAggregator;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ISessionClient _sessionClient;

        public MainPageMasterViewModel MainPageMasterViewModel { get; }

        private DetailPageViewModel _detailPageViewModel;

        public DetailPageViewModel DetailPageViewModel
        {
            get => _detailPageViewModel;
            set
            {
                _detailPageViewModel = value;
                OnPropertyChanged(nameof(DetailPageViewModel));
            }
        }

        private bool _isPresented;

        public bool IsPresented
        {
            get => _isPresented;
            set
            {
                _isPresented = value;
                OnPropertyChanged(nameof(IsPresented));
            }
        }


        private SessionState _sessionState;

        public SessionState SessionState
        {
            get => _sessionState;
            set
            {
                _sessionState = value;

                if (DetailPageViewModel is HomeViewModel homeViewModel)
                {
                    homeViewModel.SessionState = value;
                }

                OnPropertyChanged(nameof(SessionState));
            }
        }


        public MainPageViewModel(
            IViewModelFactory viewModelFactory,
            ISessionClient sessionClient,
            IEventAggregator eventAggregator)
        {
            _sessionClient = sessionClient;

            MainPageMasterViewModel = viewModelFactory.Get<MainPageMasterViewModel>();
            DetailPageViewModel = viewModelFactory.Get<HomeViewModel>();

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    SessionState = SessionState.None;
                    await Task.Delay(2000);
                    SessionState = SessionState.Active;
                    await Task.Delay(2000);
                }
            });

            eventAggregator.GetEvent<NavigationItemSelected>().Subscribe(e =>
            {
                switch (e.Type)
                {
                    case NavigationType.Home:
                        DetailPageViewModel = viewModelFactory.Get<HomeViewModel>();
                        break;
                    case NavigationType.Settings:
                        DetailPageViewModel = viewModelFactory.Get<SettingsViewModel>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                IsPresented = false;
            });
        }

        public async Task OnAppearing()
        {
            var activeSession = await _sessionClient.GetActiveSession();
            SessionState = activeSession == null ? SessionState.None : SessionState.Active;
        }
    }
}