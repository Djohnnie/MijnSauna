using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.Factories.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels.Base;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ISessionClient _sessionClient;

        public MainPageMasterViewModel MainPageMasterViewModel { get; }
        public DetailPageViewModel DetailPageViewModel { get; }


        private SessionState _sessionState;

        public SessionState SessionState
        {
            get => _sessionState;
            set
            {
                _sessionState = value;
                OnPropertyChanged(nameof(SessionState));
            }
        }


        public MainPageViewModel(
            IViewModelFactory viewModelFactory,
            ISessionClient sessionClient)
        {
            _viewModelFactory = viewModelFactory;
            _sessionClient = sessionClient;

            MainPageMasterViewModel = _viewModelFactory.Get<MainPageMasterViewModel>();
        }

        public async Task OnAppearing()
        {
            var activeSession = await _sessionClient.GetActiveSession();
            SessionState = activeSession == null ? SessionState.None : SessionState.Active;
        }
    }
}