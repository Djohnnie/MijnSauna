using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Frontend.Maui.Blazor.Platforms.Windows.Configuration;

public class ClientConfiguration : IClientConfiguration
{
    #region <| ServiceBaseUrl |>

    private const string IdServiceBaseUrl = "service_base_url";
    private static readonly string DefaultServiceBaseUrl = string.Empty;

    public string ServiceBaseUrl
    {
        get => "test";
        set => _ = value;
    }

    #endregion

    #region <| ClientId |>

    private const string IdClientId = "client_id";
    private static readonly string DefaultClientId = string.Empty;

    public string ClientId
    {
        get => "test";
        set => _ = value;
    }

    #endregion

    #region <| IsSaunaMode |>

    private const string IdIsSaunaMode = "sauna_mode";
    private static readonly string DefaultIsSaunaMode = string.Empty;

    public bool IsSaunaMode
    {

        get => false;
        set => _ = value;
    }

    #endregion
}