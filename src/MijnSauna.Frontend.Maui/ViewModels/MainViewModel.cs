using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Frontend.Maui.Enums;
using MijnSauna.Frontend.Maui.Services;
using MijnSauna.Frontend.Maui.ViewModels.Base;

namespace MijnSauna.Frontend.Maui.ViewModels;

public class MainViewModel : ViewModelBase
{
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

    #region <| Construction |>

    public MainViewModel(IMediaService mediaService)
    {
        SessionState = SessionState.None;
        Temperature = "99 °C";
        Date = "woensdag 1 januari 2022";
        Time = "16:45";
        OutsideTemperature = "6 °C";
        Countdown = "10:11";
        PowerUsage = "6099 W";

        mediaService.RegisterCallback(mediaInfo =>
        {
            MediaInfo = mediaInfo == null ? string.Empty : $"{mediaInfo.Artist} - {mediaInfo.Track}";
        });
    }

    #endregion
}