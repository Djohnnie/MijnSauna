using System;
using System.Collections.Generic;
using System.Linq;
using MijnSauna.Common.Client.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Frontend.Phone.Enums;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;
using MijnSauna.Frontend.Phone.ViewModels.Events;
using MijnSauna.Frontend.Phone.ViewModels.Helpers;
using Reactive.EventAggregator;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class HomeViewModel : DetailPageViewModel
    {
        #region <| Dependencies |>

        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionClient _sessionClient;
        private readonly ISampleClient _sampleClient;
        private readonly ISensorClient _sensorClient;

        #endregion

        #region <| Properties - ActiveSession |>

        private GetActiveSessionResponse _activeSession;

        public GetActiveSessionResponse ActiveSession
        {
            get => _activeSession;
            set
            {
                _activeSession = value;
                OnPropertyChanged(nameof(ActiveSession));
            }
        }

        #endregion

        #region <| Properties - PowerUsage |>

        private string _powerUsage;

        public string PowerUsage
        {
            get => _powerUsage;
            set
            {
                _powerUsage = value;
                OnPropertyChanged(nameof(PowerUsage));
            }
        }

        #endregion

        #region <| Properties - SaunaTemperature |>

        private string _saunaTemperature;

        public string SaunaTemperature
        {
            get => _saunaTemperature;
            set
            {
                _saunaTemperature = value;
                OnPropertyChanged(nameof(SaunaTemperature));
            }
        }

        #endregion

        #region <| Properties - OutsideTemperature |>

        private string _outsideTemperature;

        public string OutsideTemperature
        {
            get => _outsideTemperature;
            set
            {
                _outsideTemperature = value;
                OnPropertyChanged(nameof(OutsideTemperature));
            }
        }

        #endregion

        #region <| Properties - CurrentTime |>

        private string _currentTime;

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        #endregion

        #region <| Properties - Countdown |>

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

        #endregion

        #region <| Properties - Temperatures |>

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

        #endregion

        #region <| Commands |>

        public ICommand CreateSessionCommand { get; }

        #endregion

        #region <| Construction |>

        public HomeViewModel(
            IEventAggregator eventAggregator,
            ISessionClient sessionClient,
            ISampleClient sampleClient,
            ITimerHelper timerHelper,
            ISensorClient sensorClient)
        {
            _eventAggregator = eventAggregator;
            _sessionClient = sessionClient;
            _sampleClient = sampleClient;
            _sensorClient = sensorClient;

            Title = "Overzicht";
            Temperatures = new List<int>();

            timerHelper.Start(OnPolling, 10000);
            timerHelper.Start(OnCountdown, 1000);

            CreateSessionCommand = new Command(OnCreateSession);
        }

        #endregion

        #region <| Timer Handlers |>

        private Task OnPolling()
        {
            return RefreshActiveSession();
        }

        private Task OnCountdown()
        {
            return RefreshData();
        }

        #endregion

        #region <| Command Handlers |>

        private void OnCreateSession()
        {
            _eventAggregator.Publish(new NavigationItemSelected
            {
                Type = NavigationType.CreateSession
            });
        }

        #endregion

        #region <| Helper Methods |>

        private async Task RefreshActiveSession()
        {
            var currentDateAndTime = DateTime.Now;
            Temperatures = new List<int>();

            ActiveSession = await _sessionClient.GetActiveSession();
            SessionState = ActiveSession != null ? SessionState.Active : SessionState.None;

            var powerUsage = await _sensorClient.GetPowerUsage();
            PowerUsage = $"{powerUsage.PowerUsage} W";
            var saunaTemperature = await _sensorClient.GetSaunaTemperature();
            SaunaTemperature = $"{saunaTemperature.Temperature} °C";
            var outsideTemperature = await _sensorClient.GetOutsideTemperature();
            OutsideTemperature = $"{outsideTemperature.Temperature} °C";
            CurrentTime = $"{DateTime.Now:HH:mm}";

            if (_activeSession != null)
            {
                var samples = await _sampleClient.GetSamplesForSession(_activeSession.SessionId);
                if (samples != null)
                {
                    Temperatures = samples.Samples.Select(x => x.Temperature).ToList();
                }
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

        #endregion


    }
}