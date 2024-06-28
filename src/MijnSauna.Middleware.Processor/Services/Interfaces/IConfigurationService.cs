using MijnSauna.Common.DataTransferObjects.Configuration;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface IConfigurationService
    {
        bool IsConfigured { get; set; }
        string TemperatureModulesW1 { get; set; }
        string TemperatureModuleW1 { get; set; }
        int SaunaOutputGpioPin { get; set; }
        int SaunaInputGpioPin { get; set; }
        int InfraredOutputGpioPin { get; set; }
        int InfraredInputGpioPin { get; set; }
        int ShowerHeatingOutputGpioPin { get; set; }
        int ShowerHeatingThresholdTemperature { get; set; }
        int SaunaHysteresis { get; set; }
        int InfraredHysteresis { get; set; }
        int SaunaDefaultDuration { get; set; }
        int InfraredDefaultDuration { get; set; }
        int SaunaDefaultTemperature { get; set; }
        int InfraredDefaultTemperature { get; set; }

        void UpdateConfiguration(GetConfigurationValuesResponse configuration);
    }
}