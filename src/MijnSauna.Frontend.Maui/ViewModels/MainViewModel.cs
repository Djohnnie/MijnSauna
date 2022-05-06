using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Frontend.Maui.Enums;
using MijnSauna.Frontend.Maui.Helpers.Interfaces;
using MijnSauna.Frontend.Maui.Services;
using MijnSauna.Frontend.Maui.ViewModels.Base;
using SkiaSharp;

namespace MijnSauna.Frontend.Maui.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region <| Dependencies |>

    private readonly ISensorClient _sensorClient;
    private readonly ISessionClient _sessionClient;
    private readonly ISampleClient _sampleClient;
    //private readonly ISoundService _soundService;

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

    private ObservableCollection<ISeries> _temperatures;

    public ObservableCollection<ISeries> Temperatures
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

    public LiveChartsCore.Measure.Margin DrawMargin { get; set; } = new LiveChartsCore.Measure.Margin(0);

    public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                ShowSeparatorLines = false,
                IsVisible = false
            }
        };

    public Axis[] YAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                ShowSeparatorLines = false,
                IsVisible = false,
                MinLimit = 0,
                MaxLimit = 120
            }
        };

    #region <| Commands |>

    public ICommand QuickStartSaunaCommand { get; set; }
    public ICommand QuickStartInfraredCommand { get; set; }
    public ICommand CancelCommand { get; }

    #endregion

    private readonly ObservableCollection<ObservableValue> _observableValues = new ObservableCollection<ObservableValue>();

    #region <| Construction |>

    public MainViewModel(
        IClientConfiguration clientConfiguration,
        IMediaService mediaService,
        ISensorClient sensorClient,
        ISessionClient sessionClient,
        ISampleClient sampleClient,
        ITimerHelper timerHelper)
    {
        _sensorClient = sensorClient;
        _sessionClient = sessionClient;
        _sampleClient = sampleClient;


        SessionState = SessionState.None;
        Temperature = "99 °C";
        Date = "woensdag 1 januari 2022";
        Time = "16:45";
        OutsideTemperature = "6 °C";
        Countdown = "10:11";
        PowerUsage = "6099 W";

        timerHelper.Start(OnPolling, 10000);
        timerHelper.Start(OnCountdown, 1000);
        timerHelper.Start(OnProgress, 100);

        Temperatures = new ObservableCollection<ISeries>
        {
            new LineSeries<ObservableValue>
            {
                Values = _observableValues,
                Fill = new SolidColorPaint(new SKColor(220,20,60)),
                Stroke = new SolidColorPaint(SKColors.Black, 5),
                GeometrySize = 0,
                LineSmoothness = 1
            }
        };



        mediaService.RegisterCallback(mediaInfo =>
        {
            MediaInfo = mediaInfo == null ? string.Empty : $"{mediaInfo.Artist} - {mediaInfo.Track}";
        });

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

        //_soundService.PlayClickSound();
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

        //_soundService.PlayClickSound();
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

        //_soundService.PlayClickSound();
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
        try
        {
            var currentDateAndTime = DateTime.Now;

            ActiveSession = await _sessionClient.GetActiveSession();
            SessionState = ActiveSession != null ? SessionState.Active : SessionState.None;

            if (ActiveSession == null)
            {
                _observableValues.Clear();
            }

            Date = $"{currentDateAndTime:dddd d MMMM yyyy}";
            Time = $"{currentDateAndTime:HH:mm}";

            var temperature = await _sensorClient.GetSaunaTemperature();
            Temperature = temperature != null ? $"{temperature.Temperature} °C" : "???";
            var outsideTemperature = await _sensorClient.GetOutsideTemperature();
            OutsideTemperature = outsideTemperature != null ? $"{outsideTemperature.Temperature} °C" : "???";
            var powerUsage = await _sensorClient.GetSaunaPowerUsage();
            PowerUsage = powerUsage != null
                ? $"{(powerUsage.SaunaPowerUsage + powerUsage.InfraredPowerUsage):F0} W"
                : "???";

            if (ActiveSession != null)
            {
                var samples = await _sampleClient.GetSamplesForSession(ActiveSession.SessionId);

                if (samples != null)
                {
                    _observableValues.Clear();

                    foreach (var sample in samples.Samples)
                    {
                        _observableValues.Add(new ObservableValue(sample.Temperature*2));
                    }
                }
                else
                {
                    _observableValues.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
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