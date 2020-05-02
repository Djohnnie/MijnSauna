using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISampleClient _sampleClient;
        private readonly ISoundService _soundService;
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

        private List<int> _temperatures;

        public List<int> Temperatures
        {
            get => _temperatures;
            set
            {
                _temperatures = value;
                OnPropertyChanged(nameof(Temperatures));
            }
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
            ISampleClient sampleClient,
            ISoundService soundService)
        {
            _timerHelper = timerHelper;
            _sensorClient = sensorClient;
            _sessionClient = sessionClient;
            _sampleClient = sampleClient;
            _soundService = soundService;

            _timerHelper.Start(OnTimer, 10000);
            _timerHelper.Start(OnCountdown, 500);

            QuickStartSaunaCommand = new Command(async () => await OnQuickStartSauna());
            QuickStartInfraredCommand = new Command(async () => await OnQuickStartInfrared());
            CancelCommand = new Command(async () => await OnCancel());
        }

        private async Task OnTimer()
        {
            var currentDateAndTime = DateTime.Now;

            _activeSession = await _sessionClient.GetActiveSession();
            SessionState = _activeSession != null ? SessionState.Active : SessionState.None;

            var temperature = await _sensorClient.GetSaunaTemperature();

            Date = $"{currentDateAndTime:dddd d MMMM yyyy}";
            Time = $"{currentDateAndTime:HH:mm}";
            Temperature = $"{temperature.Temperature} °C";

            if (_activeSession != null)
            {
                var samples = await _sampleClient.GetSamplesForSession(_activeSession.SessionId);
                Temperatures = samples.Samples.Select(x => x.Temperature).ToList();
            }
        }

        private Task OnCountdown()
        {
            return RefreshData();
        }

        private async Task OnQuickStartSauna()
        {
            _soundService.PlayClickSound();
            await _sessionClient.QuickStartSession(new QuickStartSessionRequest
            {
                IsSauna = true
            });
        }

        private async Task OnQuickStartInfrared()
        {
            _soundService.PlayClickSound();
            await _sessionClient.QuickStartSession(new QuickStartSessionRequest
            {
                IsInfrared = true
            });
        }

        private async Task OnCancel()
        {
            _soundService.PlayClickSound();
            if (_activeSession != null)
            {
                await _sessionClient.CancelSession(_activeSession.SessionId);
                await RefreshData();
            }
        }

        private Task RefreshData()
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
    }
}