using MijnSauna.Common.Client.Interfaces;
using static System.Net.WebRequestMethods;

namespace MijnSauna.Frontend.Maui.Configuration;

public class ClientConfiguration : IClientConfiguration
{
    #region <| ServiceBaseUrl |>

    private const string IdServiceBaseUrl = "service_base_url";
    private static readonly string DefaultServiceBaseUrl = "";

    public string ServiceBaseUrl
    {
        get => Preferences.Get(IdServiceBaseUrl, DefaultServiceBaseUrl);
        set => Preferences.Set(IdServiceBaseUrl, value);
    }

    #endregion

    #region <| ClientId |>

    private const string IdClientId = "client_id";
    private static readonly string DefaultClientId = "";

    public string ClientId
    {
        get => Preferences.Get(IdClientId, DefaultClientId);
        set => Preferences.Set(IdClientId, value);
    }

    #endregion

    #region <| IsSaunaMode |>

    private const string IdIsSaunaMode = "sauna_mode";
    private static readonly string DefaultIsSaunaMode = string.Empty;

    public bool IsSaunaMode
    {
        get => Preferences.Get(IdIsSaunaMode, DefaultIsSaunaMode) == "TRUE";
        set => Preferences.Set(IdIsSaunaMode, value ? "TRUE" : "FALSE");
    }

    #endregion
}