using MijnSauna.Common.Client.Interfaces;
using System.Threading.Tasks;

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

        public HomeViewModel(ISensorClient sensorClient)
        {
            Title = "Mijn Sauna";

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