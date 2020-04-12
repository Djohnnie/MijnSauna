using MijnSauna.Common.Client.Interfaces;
using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Enums;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class HomeViewModel : DetailPageViewModel
    {
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

        public HomeViewModel(ISensorClient sensorClient)
        {
            Title = "Hoofdscherm!!!";

            Task.Run(async () =>
            {
                while (true)
                {
                    var powerUsage = await sensorClient.GetPowerUsage();
                    PowerUsage = $"{powerUsage.PowerUsage} W";
                    var outsideTemperature = await sensorClient.GetOutsideTemperature();
                    OutsideTemperature = $"{outsideTemperature.Temperature} °C";
                    await Task.Delay(10000);
                }
            });
        }
    }
}