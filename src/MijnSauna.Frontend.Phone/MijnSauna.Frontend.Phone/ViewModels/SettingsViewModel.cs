using MijnSauna.Common.Client.Interfaces;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class SettingsViewModel : DetailPageViewModel
    {
        private readonly IClientConfiguration _clientConfiguration;

        public string ServiceBaseUrl
        {
            get => _clientConfiguration.ServiceBaseUrl ?? string.Empty;
            set => _clientConfiguration.ServiceBaseUrl = value;
        }

        public string ClientId
        {
            get => _clientConfiguration.ClientId ?? string.Empty;
            set => _clientConfiguration.ClientId = value;
        }

        public bool IsSaunaMode
        {
            get => _clientConfiguration.IsSaunaMode;
            set => _clientConfiguration.IsSaunaMode = value;
        }

        public SettingsViewModel(IClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            Title = "Instellingen";
        }
    }
}