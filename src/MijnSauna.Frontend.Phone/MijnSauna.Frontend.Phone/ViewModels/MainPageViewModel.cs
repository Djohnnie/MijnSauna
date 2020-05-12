using System;
using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.Factories.Interfaces;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;
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
            IEventAggregator eventAggregator,
            IClientConfiguration clientConfiguration,
            ITimerHelper timerHelper)
        {
            _sessionClient = sessionClient;

            MainPageMasterViewModel = viewModelFactory.Get<MainPageMasterViewModel>();

            if (!string.IsNullOrEmpty(clientConfiguration.ServiceBaseUrl) &&
                !string.IsNullOrEmpty(clientConfiguration.ClientId))
            {
                DetailPageViewModel = viewModelFactory.Get<HomeViewModel>();
            }
            else
            {
                DetailPageViewModel = viewModelFactory.Get<SettingsViewModel>();
            }

            timerHelper.Start(OnPolling, 10000);

            eventAggregator.GetEvent<NavigationItemSelected>().Subscribe(e =>
            {
                switch (e.Type)
                {
                    case NavigationType.Home:
                        DetailPageViewModel = viewModelFactory.Get<HomeViewModel>();
                        break;
                    case NavigationType.CreateSession:
                        DetailPageViewModel = viewModelFactory.Get<CreateSessionViewModel>();
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

        private async Task OnPolling()
        {
            var activeSession = await _sessionClient.GetActiveSession();
            SessionState = activeSession == null ? SessionState.None : SessionState.Active;
        }
    }
}