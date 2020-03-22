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
    }
}