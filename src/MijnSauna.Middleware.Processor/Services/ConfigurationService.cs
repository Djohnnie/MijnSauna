using System;
using System.Linq;
using MijnSauna.Common.DataTransferObjects.Configuration;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string TEMPERATURE_MODULES_W1 = nameof(TEMPERATURE_MODULES_W1);
        private const string TEMPERATURE_MODULE_W1 = nameof(TEMPERATURE_MODULE_W1);
        private const string SAUNA_OUTPUT_GPIO_PIN = nameof(SAUNA_OUTPUT_GPIO_PIN);
        private const string SAUNA_INPUT_GPIO_PIN = nameof(SAUNA_INPUT_GPIO_PIN);
        private const string INFRARED_OUTPUT_GPIO_PIN = nameof(INFRARED_OUTPUT_GPIO_PIN);
        private const string INFRARED_INPUT_GPIO_PIN = nameof(INFRARED_INPUT_GPIO_PIN);
        private const string SHOWER_HEATING_OUTPUT_GPIO_PIN = nameof(SHOWER_HEATING_OUTPUT_GPIO_PIN);
        private const string SHOWER_HEATING_THRESHOLD_TEMPERATURE = nameof(SHOWER_HEATING_THRESHOLD_TEMPERATURE);
        private const string SAUNA_HYSTERESIS = nameof(SAUNA_HYSTERESIS);
        private const string INFRARED_HYSTERESIS = nameof(INFRARED_HYSTERESIS);
        private const string SAUNA_DEFAULT_DURATION = nameof(SAUNA_DEFAULT_DURATION);
        private const string INFRARED_DEFAULT_DURATION = nameof(INFRARED_DEFAULT_DURATION);
        private const string SAUNA_DEFAULT_TEMPERATURE = nameof(SAUNA_DEFAULT_TEMPERATURE);
        private const string INFRARED_DEFAULT_TEMPERATURE = nameof(INFRARED_DEFAULT_TEMPERATURE);

        public bool IsConfigured { get; set; }
        public string TemperatureModulesW1 { get; set; }
        public string TemperatureModuleW1 { get; set; }
        public int SaunaOutputGpioPin { get; set; }
        public int SaunaInputGpioPin { get; set; }
        public int InfraredOutputGpioPin { get; set; }
        public int InfraredInputGpioPin { get; set; }
        public int ShowerHeatingOutputGpioPin { get; set; }
        public int ShowerHeatingThresholdTemperature { get; set; }
        public int SaunaHysteresis { get; set; }
        public int InfraredHysteresis { get; set; }
        public int SaunaDefaultDuration { get; set; }
        public int InfraredDefaultDuration { get; set; }
        public int SaunaDefaultTemperature { get; set; }
        public int InfraredDefaultTemperature { get; set; }

        public void UpdateConfiguration(GetConfigurationValuesResponse configuration)
        {
            TemperatureModulesW1 = ToString(configuration, TEMPERATURE_MODULES_W1);
            TemperatureModuleW1 = ToString(configuration, TEMPERATURE_MODULE_W1);
            SaunaOutputGpioPin = ToInt32(configuration, SAUNA_OUTPUT_GPIO_PIN);
            SaunaInputGpioPin = ToInt32(configuration, SAUNA_INPUT_GPIO_PIN);
            InfraredOutputGpioPin = ToInt32(configuration, INFRARED_OUTPUT_GPIO_PIN);
            InfraredInputGpioPin = ToInt32(configuration, INFRARED_INPUT_GPIO_PIN);
            ShowerHeatingOutputGpioPin = ToInt32(configuration, SHOWER_HEATING_OUTPUT_GPIO_PIN);
            ShowerHeatingThresholdTemperature = ToInt32(configuration, SHOWER_HEATING_THRESHOLD_TEMPERATURE);
            SaunaHysteresis = ToInt32(configuration, SAUNA_HYSTERESIS);
            InfraredHysteresis = ToInt32(configuration, INFRARED_HYSTERESIS);
            SaunaDefaultDuration = ToInt32(configuration, SAUNA_DEFAULT_DURATION);
            InfraredDefaultDuration = ToInt32(configuration, INFRARED_DEFAULT_DURATION);
            SaunaDefaultTemperature = ToInt32(configuration, SAUNA_DEFAULT_TEMPERATURE);
            InfraredDefaultTemperature = ToInt32(configuration, INFRARED_DEFAULT_TEMPERATURE);

            IsConfigured = true;
        }

        private string ToString(GetConfigurationValuesResponse configuration, string configurationName)
        {
            return configuration.ConfigurationValues.Single(x => x.Name == configurationName).Value;
        }

        private int ToInt32(GetConfigurationValuesResponse configuration, string configurationName)
        {
            return Convert.ToInt32(
                configuration.ConfigurationValues.Single(x => x.Name == configurationName).Value);
        }
    }
}