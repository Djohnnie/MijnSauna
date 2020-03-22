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
    }
}