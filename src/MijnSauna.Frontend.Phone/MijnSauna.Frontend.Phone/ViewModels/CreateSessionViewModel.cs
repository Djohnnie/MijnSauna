using System.Windows.Input;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class CreateSessionViewModel : DetailPageViewModel
    {
        private string _test;

        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }

        public ICommand SelectSaunaCommand { get; }
        public ICommand SelectInfraredCommand { get; }

        public CreateSessionViewModel()
        {
            Title = "Nieuwe sessie";

            SelectSaunaCommand = new Command(OnSelectSauna);
            SelectInfraredCommand = new Command(OnSelectInfrared);
        }

        private void OnSelectSauna()
        {
            Test = "SELECT_SAUNA";
        }

        private void OnSelectInfrared()
        {
            Test = "SELECT_INFRARED";
        }
    }
}