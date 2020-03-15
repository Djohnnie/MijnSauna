using System.ComponentModel;
using System.Runtime.CompilerServices;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Enums;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IStatusBarService _statusBarService;


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


        public MainPageViewModel(IStatusBarService statusBarService)
        {
            _statusBarService = statusBarService;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(2500);
                    SessionState = SessionState.Active;
                    await Task.Delay(2500);
                    SessionState = SessionState.None;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}