using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.ViewModels.Base;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

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
    }
}