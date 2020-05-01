using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;
using MijnSauna.Frontend.Phone.Services.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels.Base;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class SaunaPageViewModel : ViewModelBase
    {
        private readonly ITimerHelper _timerHelper;
        private readonly ISensorClient _sensorClient;
        private readonly ISessionClient _sessionClient;
        private readonly ISoundService _soundService;
        private List<int> _temperatures = new List<int>();
        private GetActiveSessionResponse _activeSession;

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

        public List<int> Temperatures
        {
            get => _temperatures;
        }


        private string _date;

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _time;

        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _temperature;

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private string _countdown;

        public string Countdown
        {
            get => _countdown;
            set
            {
                _countdown = value;
                OnPropertyChanged(nameof(Countdown));
            }
        }


        public ICommand QuickStartSaunaCommand { get; set; }
        public ICommand QuickStartInfraredCommand { get; set; }
        public ICommand CancelCommand { get; }

        public SaunaPageViewModel(
            ITimerHelper timerHelper,
            ISensorClient sensorClient,
            ISessionClient sessionClient,
            ISoundService soundService)
        {
            _timerHelper = timerHelper;
            _sensorClient = sensorClient;
            _sessionClient = sessionClient;

            _timerHelper.Start(OnTimer, 10000);
            _timerHelper.Start(OnCountdown, 500);

            _soundService = soundService;
            _temperatures.Add(23);
            _temperatures.Add(28);
            _temperatures.Add(31);
            _temperatures.Add(36);
            _temperatures.Add(41);
            _temperatures.Add(42);
            _temperatures.Add(43);
            _temperatures.Add(56);
            _temperatures.Add(66);
            _temperatures.Add(71);
            _temperatures.Add(90);
            _temperatures.Add(93);
            _temperatures.Add(101);
            _temperatures.Add(115);
            _temperatures.Add(120);
            _temperatures.Add(115);
            _temperatures.Add(116);
            _temperatures.Add(119);
            _temperatures.Add(118);
            _temperatures.Add(111);
            _temperatures.Add(120);

            QuickStartSaunaCommand = new Command(OnQuickStartSauna);
            QuickStartInfraredCommand = new Command(OnQuickStartInfrared);
            CancelCommand = new Command(OnCancel);
        }

        private async Task OnTimer()
        {
            var currentDateAndTime = DateTime.Now;

            _activeSession = await _sessionClient.GetActiveSession();
            SessionState = _activeSession != null ? SessionState.Active : SessionState.None;
            Description = _activeSession != null ? _activeSession.IsSauna ? "sauna" : "infrarood" : String.Empty;


            var temperature = await _sensorClient.GetSaunaTemperature();

            Date = $"{currentDateAndTime:dddd d MMMM yyyy}";
            Time = $"{currentDateAndTime:HH:mm}";
            Temperature = $"{temperature.Temperature} °C";
        }

        private Task OnCountdown()
        {
            if (_activeSession != null)
            {
                var timeDifference = _activeSession.End.ToLocalTime() - DateTime.Now;
                if (timeDifference > TimeSpan.Zero)
                {
                    Countdown = timeDifference > TimeSpan.FromHours(1) ? $"{timeDifference:hh\\:mm\\:ss}" : $"{timeDifference:mm\\:ss}";
                }
                else
                {
                    Countdown = "00:00";
                }
            }
            else
            {
                Countdown = string.Empty;
            }
            return Task.CompletedTask;
        }

        private void OnQuickStartSauna()
        {
            _soundService.PlayClickSound();
        }

        private void OnQuickStartInfrared()
        {
            _soundService.PlayClickSound();
        }

        private void OnCancel()
        {
            _soundService.PlayClickSound();
            if (_activeSession != null)
            {
                _sessionClient.CancelSession(_activeSession.SessionId);
            }
        }
    }
}