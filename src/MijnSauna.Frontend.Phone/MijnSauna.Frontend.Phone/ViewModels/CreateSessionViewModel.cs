using System;
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

        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private TimeSpan _stopTime;

        public TimeSpan StopTime
        {
            get => _stopTime;
            set
            {
                _stopTime = value;
                OnPropertyChanged(nameof(StopTime));
            }
        }

        public ICommand SelectSaunaCommand { get; }
        public ICommand SelectInfraredCommand { get; }

        public CreateSessionViewModel()
        {
            Title = "Nieuwe sessie";
            StartTime = new TimeSpan(DateTime.Now.Hour, (int)(DateTime.Now.Minute / 5f + 1) * 5, 0);
            StopTime = StartTime.Add(TimeSpan.FromMinutes(60));

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