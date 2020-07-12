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
        #region <| Dependencies |>

        private readonly ISensorClient _sensorClient;
        private readonly ISessionClient _sessionClient;
        private readonly ISampleClient _sampleClient;
        private readonly ISoundService _soundService;

        #endregion

        #region <| Private Members |>

        private bool _isSaunaPending;
        private bool _isInfraredPending;
        private bool _isCancelPending;

        #endregion

        #region <| Properties - SessionState |>

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

        #region <| Properties - Date |>

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

        #endregion

        #region <| Properties - Time |>

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

        #endregion

        #region <| Properties - Temperature |>

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

        #region <| Properties - SaunaCaption |>

        private string _saunaCaption;

        public string SaunaCaption
        {
            get => _saunaCaption;
            set
            {
                _saunaCaption = value;
                OnPropertyChanged(nameof(SaunaCaption));
            }
        }

        #endregion

        #region <| Properties - InfraredCaption |>

        private string _infraredCaption;

        public string InfraredCaption
        {
            get => _infraredCaption;
            set
            {
                _infraredCaption = value;
                OnPropertyChanged(nameof(InfraredCaption));
            }
        }

        #endregion

        #region <| Properties - CancelCaption |>

        private string _cancelCaption;

        public string CancelCaption
        {
            get => _cancelCaption;
            set
            {
                _cancelCaption = value;
                OnPropertyChanged(nameof(CancelCaption));
            }
        }

        #endregion

        #region <| Properties - MediaInfo |>

        private string _mediaInfo;

        public string MediaInfo
        {
            get => _mediaInfo;
            set
            {
                _mediaInfo = value;
                OnPropertyChanged(nameof(MediaInfo));
            }
        }

        #endregion

        #region <| Commands |>

        public ICommand QuickStartSaunaCommand { get; set; }
        public ICommand QuickStartInfraredCommand { get; set; }
        public ICommand CancelCommand { get; }

        #endregion

        #region <| Construction |>

        public SaunaPageViewModel(
            ITimerHelper timerHelper,
            ISensorClient sensorClient,
            ISessionClient sessionClient,
            ISampleClient sampleClient,
            ISoundService soundService,
            IMediaService mediaService)
        {
            _sensorClient = sensorClient;
            _sessionClient = sessionClient;
            _sampleClient = sampleClient;
            _soundService = soundService;

            mediaService.RegisterCallback(mediaInfo =>
            {
                MediaInfo = mediaInfo == null ? string.Empty : $"{mediaInfo.Artist} - {mediaInfo.Track}";
            });

            timerHelper.Start(OnPolling, 10000);
            timerHelper.Start(OnCountdown, 1000);
            timerHelper.Start(OnProgress, 100);

            QuickStartSaunaCommand = new Command(async () => await OnQuickStartSauna());
            QuickStartInfraredCommand = new Command(async () => await OnQuickStartInfrared());
            CancelCommand = new Command(async () => await OnCancel());
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

        private int _counter;

        private Task OnProgress()
        {
            string pending = "\ue80d";

            if (_counter > 3)
            {
                _counter = 0;
            }

            if (_counter == 0)
            {
                pending = "\ue80d";
            }
            if (_counter == 1)
            {
                pending = "\ue80e";
            }
            if (_counter == 2)
            {
                pending = "\ue80f";
            }
            if (_counter == 3)
            {
                pending = "\u0e80";
            }

            _counter++;

            if (_isSaunaPending)
            {
                SaunaCaption = pending;
            }
            else
            {
                SaunaCaption = "\ue812";
            }

            if (_isInfraredPending)
            {
                InfraredCaption = pending;
            }
            else
            {
                InfraredCaption = "\ue807";
            }

            if (_isCancelPending)
            {
                CancelCaption = pending;
            }
            else
            {
                CancelCaption = "\ue806";
            }

            return Task.CompletedTask;
        }

        #endregion

        #region <| Command Handlers |>

        private async Task OnQuickStartSauna()
        {
            _isSaunaPending = true;

            _soundService.PlayClickSound();
            await _sessionClient.QuickStartSession(new QuickStartSessionRequest
            {
                IsSauna = true
            });
            await Task.Delay(1000);
            await RefreshActiveSession();

            _isSaunaPending = false;
        }

        private async Task OnQuickStartInfrared()
        {
            _isInfraredPending = true;

            _soundService.PlayClickSound();
            await _sessionClient.QuickStartSession(new QuickStartSessionRequest
            {
                IsInfrared = true
            });
            await Task.Delay(1000);
            await RefreshActiveSession();

            _isInfraredPending = false;
        }

        private async Task OnCancel()
        {
            _isCancelPending = true;

            _soundService.PlayClickSound();
            if (_activeSession != null)
            {
                await _sessionClient.CancelSession(_activeSession.SessionId);
                await Task.Delay(1000);
                await RefreshActiveSession();
            }

            _isCancelPending = false;
        }

        #endregion

        #region <| Helper Methods |>

        private async Task RefreshActiveSession()
        {
            var currentDateAndTime = DateTime.Now;

            ActiveSession = await _sessionClient.GetActiveSession();
            SessionState = ActiveSession != null ? SessionState.Active : SessionState.None;

            if (ActiveSession == null)
            {
                Temperatures = new List<int>();
            }

            Date = $"{currentDateAndTime:dddd d MMMM yyyy}";
            Time = $"{currentDateAndTime:HH:mm}";

            var temperature = await _sensorClient.GetSaunaTemperature();
            Temperature = temperature != null ? $"{temperature.Temperature} °C" : "???";
            var outsideTemperature = await _sensorClient.GetOutsideTemperature();
            OutsideTemperature = outsideTemperature != null ? $"{outsideTemperature.Temperature} °C" : "???";
            var powerUsage = await _sensorClient.GetPowerUsage();
            PowerUsage = powerUsage != null ? $"{powerUsage.PowerUsage} W" : "???";

            if (ActiveSession != null)
            {
                List<int> temperatures = null;
                var samples = await _sampleClient.GetSamplesForSession(ActiveSession.SessionId);
                if (samples != null)
                {
                    temperatures = samples.Samples.Select(x => x.Temperature).ToList();
                }

                if (temperatures != null && temperatures.Count > 0)
                {
                    Temperatures = temperatures;
                }
            }
        }

        private Task RefreshData()
        {
            if (ActiveSession != null)
            {
                var timeDifference = ActiveSession.End.ToLocalTime() - DateTime.Now;
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