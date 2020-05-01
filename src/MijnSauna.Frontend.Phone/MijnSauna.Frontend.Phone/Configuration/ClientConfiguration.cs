using MijnSauna.Common.Client.Interfaces;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MijnSauna.Frontend.Phone.Configuration
{
    public class ClientConfiguration : IClientConfiguration
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region <| ServiceBaseUrl |>

        private const string IdServiceBaseUrl = "service_base_url";
        private static readonly string DefaultServiceBaseUrl = string.Empty;

        public string ServiceBaseUrl
        {
            get => AppSettings.GetValueOrDefault(IdServiceBaseUrl, DefaultServiceBaseUrl);
            set => AppSettings.AddOrUpdateValue(IdServiceBaseUrl, value);
        }

        #endregion

        #region <| ClientId |>

        private const string IdClientId = "client_id";
        private static readonly string DefaultClientId = string.Empty;

        public string ClientId
        {
            get => AppSettings.GetValueOrDefault(IdClientId, DefaultClientId);
            set => AppSettings.AddOrUpdateValue(IdClientId, value);
        }

        #endregion

        #region <| IsSaunaMode |>

        private const string IdIsSaunaMode = "sauna_mode";
        private static readonly string DefaultIsSaunaMode = string.Empty;

        public bool IsSaunaMode
        {
            get => AppSettings.GetValueOrDefault(IdIsSaunaMode, DefaultIsSaunaMode) == "TRUE";
            set => AppSettings.AddOrUpdateValue(IdIsSaunaMode, value ? "TRUE" : "FALSE");
        }

        #endregion
    }
}