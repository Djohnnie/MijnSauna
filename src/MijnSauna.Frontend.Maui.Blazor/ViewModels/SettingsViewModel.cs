using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Frontend.Maui.Blazor.ViewModels.Base;

namespace MijnSauna.Frontend.Maui.Blazor.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IClientConfiguration _clientConfiguration;

    #region <| Properties - ServiceBaseUrl |>

    public string ServiceBaseUrl
    {
        get => _clientConfiguration.ServiceBaseUrl;
        set => _clientConfiguration.ServiceBaseUrl = value;
    }

    #endregion

    #region <| Properties - ClientId |>

    public string ClientId
    {
        get => _clientConfiguration.ClientId;
        set => _clientConfiguration.ClientId = value;
    }

    #endregion

    public SettingsViewModel(IClientConfiguration clientConfiguration)
    {
        _clientConfiguration = clientConfiguration;
    }
}