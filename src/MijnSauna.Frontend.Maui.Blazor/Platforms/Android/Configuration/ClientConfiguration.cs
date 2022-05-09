using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Frontend.Maui.Blazor.Platforms.Android.Configuration;

public class ClientConfiguration : IClientConfiguration
{
    private readonly IPreferences _appPreferences = Preferences.Default;

    #region <| ServiceBaseUrl |>

    private const string IdServiceBaseUrl = "service_base_url";
    private static readonly string DefaultServiceBaseUrl = string.Empty;

    public string ServiceBaseUrl
    {
        get => _appPreferences.Get(IdServiceBaseUrl, DefaultServiceBaseUrl);
        set => _appPreferences.Set(IdServiceBaseUrl, value);
    }

    #endregion

    #region <| ClientId |>

    private const string IdClientId = "client_id";
    private static readonly string DefaultClientId = string.Empty;

    public string ClientId
    {
        get => _appPreferences.Get(IdClientId, DefaultClientId);
        set => _appPreferences.Set(IdClientId, value);
    }

    #endregion

    #region <| IsSaunaMode |>

    private const string IdIsSaunaMode = "sauna_mode";
    private static readonly string DefaultIsSaunaMode = string.Empty;

    public bool IsSaunaMode
    {

        get => _appPreferences.Get(IdIsSaunaMode, DefaultIsSaunaMode) == "TRUE";
        set => _appPreferences.Set(IdIsSaunaMode, value ? "TRUE" : "FALSE");
    }

    #endregion
}