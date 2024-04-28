using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Frontend.Maui.Enums;
using MijnSauna.Frontend.Maui.Helpers;
using MijnSauna.Frontend.Maui.ViewModels.Base;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace MijnSauna.Frontend.Maui.ViewModels;

public class SaunaViewModel : ViewModelBase
{
    #region <| Dependencies |>

    private readonly NavigationHelper _navigationHelper;
    private readonly ISensorClient _sensorClient;
    private readonly ISessionClient _sessionClient;
    private readonly ISampleClient _sampleClient;
    private readonly IStatusBarHelper _statusBarHelper;

    //private readonly ISoundService _soundService;

    #endregion

    #region <| Private Members |>

    private byte _temperatureTapped;
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

            switch (value)
            {
                case SessionState.None:
                    _statusBarHelper.SetStatusBarColorFromRgb(106, 90, 205);
                    break;
                case SessionState.Active:
                    _statusBarHelper.SetStatusBarColorFromRgb(220, 20, 60);
                    break;
            }

            OnPropertyChanged(nameof(SessionState));
        }
    }

    private int _sessionStateFontSize;

    public int SessionStateFontSize
    {
        get => _sessionStateFontSize;
        set
        {
            _sessionStateFontSize = value;
            OnPropertyChanged(nameof(SessionStateFontSize));
        }
    }

    private int _buttonFontSize;

    public int ButtonFontSize
    {
        get => _buttonFontSize;
        set
        {
            _buttonFontSize = value;
            OnPropertyChanged(nameof(ButtonFontSize));
        }
    }

    private int _buttonSize;

    public int ButtonSize
    {
        get => _buttonSize;
        set
        {
            _buttonSize = value;
            OnPropertyChanged(nameof(ButtonSize));
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

            var values = new List<ObservablePoint>();

            for (int i = 0; i < _temperatures.Count; i++)
            {
                values.Add(new ObservablePoint { X = i, Y = _temperatures[i] + 20 });
            }

            Series[0].Values = values.ToArray();

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

    private int _dateFontSize;

    public int DateFontSize
    {
        get => _dateFontSize;
        set
        {
            _dateFontSize = value;
            OnPropertyChanged(nameof(DateFontSize));
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

    private int _timeFontSize;

    public int TimeFontSize
    {
        get => _timeFontSize;
        set
        {
            _timeFontSize = value;
            OnPropertyChanged(nameof(TimeFontSize));
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

    private int _temperatureFontSize;

    public int TemperatureFontSize
    {
        get => _temperatureFontSize;
        set
        {
            _temperatureFontSize = value;
            OnPropertyChanged(nameof(TemperatureFontSize));
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

    private int _outsideTemperatureFontSize;

    public int OutsideTemperatureFontSize
    {
        get => _outsideTemperatureFontSize;
        set
        {
            _outsideTemperatureFontSize = value;
            OnPropertyChanged(nameof(OutsideTemperatureFontSize));
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

    private int _powerUsageFontSize;

    public int PowerUsageFontSize
    {
        get => _powerUsageFontSize;
        set
        {
            _powerUsageFontSize = value;
            OnPropertyChanged(nameof(PowerUsageFontSize));
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

    private int _countdownFontSize;

    public int CountdownFontSize
    {
        get => _countdownFontSize;
        set
        {
            _countdownFontSize = value;
            OnPropertyChanged(nameof(CountdownFontSize));
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

    private int _mediaInfoFontSize;

    public int MediaInfoFontSize
    {
        get => _mediaInfoFontSize;
        set
        {
            _mediaInfoFontSize = value;
            OnPropertyChanged(nameof(MediaInfoFontSize));
        }
    }

    #endregion

    #region <| Properties - PageHeight |>

    private int _pageHeight;

    public int PageHeight
    {
        get => _pageHeight;
        set
        {
            _pageHeight = value;

            OutsideTemperatureFontSize = value / 36;
            PowerUsageFontSize = value / 36;
            DateFontSize = value / 36;
            TimeFontSize = value / 16;
            SessionStateFontSize = value / 7;
            TemperatureFontSize = value / 6;
            CountdownFontSize = value / 15;
            MediaInfoFontSize = value / 36;
            ButtonFontSize = value / 15;
            ButtonSize = value / 10;

            BottomMargin = new Thickness
            {
                Left = _bottomMargin.Left,
                Top = value / 16,
                Right = _bottomMargin.Right,
                Bottom = -value / 5
            };

            OnPropertyChanged(nameof(PageHeight));
        }
    }

    private int _pageWidth;

    public int PageWidth
    {
        get => _pageWidth;
        set
        {
            _pageWidth = value;

            BottomMargin = new Thickness
            {
                Left = -value / 3,
                Top = _bottomMargin.Top,
                Right = -value / 3,
                Bottom = _bottomMargin.Bottom
            };

            OnPropertyChanged(nameof(PageWidth));
        }
    }

    private Thickness _bottomMargin;

    public Thickness BottomMargin
    {
        get => _bottomMargin;
        set
        {
            _bottomMargin = value;
            OnPropertyChanged(nameof(BottomMargin));
        }
    }

    #endregion

    #region <| Commands |>

    public ICommand TemperatureTapCommand { get; set; }
    public ICommand QuickStartSaunaCommand { get; set; }
    public ICommand QuickStartInfraredCommand { get; set; }
    public ICommand CancelCommand { get; }

    #endregion

    #region <| Construction |>

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<ObservablePoint>
        {
            Values = new ObservablePoint[]
            {
            },
            GeometrySize = 0,
            Stroke = new SolidColorPaint(new SKColor(0,0,0,128), 4),
            LineSmoothness = 1,
            Fill = new SolidColorPaint(new SKColor(0,0,0,64))
        }
    };

    public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    ShowSeparatorLines = false,
                    LabelsPaint = new SolidColorPaint(SKColors.Transparent)
                }
            };

    public Axis[] YAxes { get; set; }
        = new Axis[]
        {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 140,
                    ShowSeparatorLines = false,
                    LabelsPaint = new SolidColorPaint(SKColors.Transparent)
                }
        };

    public SaunaViewModel(
        NavigationHelper navigationHelper,
        TimerHelper timerHelper,
        ISensorClient sensorClient,
        ISessionClient sessionClient,
        ISampleClient sampleClient,
    //ISoundService soundService,
        IStatusBarHelper statusBarHelper,
        IMediaHelper mediaHelper)
    {
        _navigationHelper = navigationHelper;
        _sensorClient = sensorClient;
        _sessionClient = sessionClient;
        _sampleClient = sampleClient;
        _statusBarHelper = statusBarHelper;
        //_soundService = soundService;

        mediaHelper.RegisterCallback(mediaInfo =>
        {
            MediaInfo = mediaInfo == null ? string.Empty : $"{mediaInfo.Artist} - {mediaInfo.Track}";
        });

        _ = statusBarHelper.KeepScreenOn(true);

        timerHelper.Start(OnPolling, 10000);
        timerHelper.Start(OnCountdown, 1000);
        timerHelper.Start(OnProgress, 100);

        TemperatureTapCommand = new Command(OnTemperatureTap);
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

    private void OnTemperatureTap()
    {
        _temperatureTapped++;

        if (_temperatureTapped >= 5)
        {
            _temperatureTapped = 0;
            _navigationHelper.NavigateToMainPage();
        }
    }

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
        _temperatureTapped = 0;

        var currentDateAndTime = DateTime.Now;

        ActiveSession = await _sessionClient.GetActiveSession();
        SessionState = ActiveSession != null ? SessionState.Active : SessionState.None;

        if (ActiveSession == null)
        {
            Temperatures = new List<int>();
        }

        Date = $"{currentDateAndTime:dddd d MMMM}";
        Time = $"{currentDateAndTime:HH:mm}";

        var temperature = await _sensorClient.GetSaunaTemperature();
        Temperature = temperature != null ? $"{temperature.Temperature} °C" : "???";
        var outsideTemperature = await _sensorClient.GetOutsideTemperature();
        OutsideTemperature = outsideTemperature != null ? $"{outsideTemperature.Temperature} °C" : "???";
        var powerUsage = await _sensorClient.GetSaunaPowerUsage();
        PowerUsage = powerUsage != null ? $"{powerUsage.SaunaPowerUsage + powerUsage.InfraredPowerUsage:F1} kW ({powerUsage.BatteryPercentage} %)" : "???";

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