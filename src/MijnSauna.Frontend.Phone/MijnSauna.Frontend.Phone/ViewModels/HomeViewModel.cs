using System;
using MijnSauna.Common.Client.Interfaces;
using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;

namespace MijnSauna.Frontend.Phone.ViewModels
{
    public class HomeViewModel : DetailPageViewModel
    {
        private readonly ITimerHelper _timerHelper;
        private readonly ISensorClient _sensorClient;

        #region <| PowerUsage |>

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

        #region <| SaunaTemperature |>

        private string _saunaTemperature;

        public string SaunaTemperature
        {
            get => _saunaTemperature;
            set
            {
                _saunaTemperature = value;
                OnPropertyChanged(nameof(SaunaTemperature));
            }
        }

        #endregion

        #region <| OutsideTemperature |>

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

        #region <| CurrentTime |>

        private string _currentTime;

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        #endregion

        public HomeViewModel(
            ITimerHelper timerHelper,
            ISensorClient sensorClient)
        {
            _timerHelper = timerHelper;
            _sensorClient = sensorClient;

            Title = "Overzicht";

            _timerHelper.Start(RefreshData, 10000);
        }

        private async Task RefreshData()
        {
            var powerUsage = await _sensorClient.GetPowerUsage();
            PowerUsage = $"{powerUsage.PowerUsage} W";
            var saunaTemperature = await _sensorClient.GetSaunaTemperature();
            SaunaTemperature = $"{saunaTemperature.Temperature} °C";
            var outsideTemperature = await _sensorClient.GetOutsideTemperature();
            OutsideTemperature = $"{outsideTemperature.Temperature} °C";
            CurrentTime = $"{DateTime.Now:HH:mm}";
        }
    }
}