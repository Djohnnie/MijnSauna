using System;
using System.Linq;
using MijnSauna.Common.DataTransferObjects.Configuration;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string TEMPERATURE_MODULES_W1 = "TEMPERATURE_MODULES_W1";
        private const string TEMPERATURE_MODULE_W1 = "TEMPERATURE_MODULE_W1";
        private const string SAUNA_OUTPUT_GPIO_PIN = "SAUNA_OUTPUT_GPIO_PIN";
        private const string SAUNA_INPUT_GPIO_PIN = "SAUNA_INPUT_GPIO_PIN";
        private const string INFRARED_OUTPUT_GPIO_PIN = "INFRARED_OUTPUT_GPIO_PIN";
        private const string INFRARED_INPUT_GPIO_PIN = "INFRARED_INPUT_GPIO_PIN";
        private const string SAUNA_HYSTERESIS = "SAUNA_HYSTERESIS";
        private const string INFRARED_HYSTERESIS = "INFRARED_HYSTERESIS";
        private const string SAUNA_DEFAULT_DURATION = "SAUNA_DEFAULT_DURATION";
        private const string INFRARED_DEFAULT_DURATION = "INFRARED_DEFAULT_DURATION";
        private const string SAUNA_DEFAULT_TEMPERATURE = "SAUNA_DEFAULT_TEMPERATURE";
        private const string INFRARED_DEFAULT_TEMPERATURE = "INFRARED_DEFAULT_TEMPERATURE";

        public string TemperatureModulesW1 { get; set; }
        public string TemperatureModuleW1 { get; set; }
        public int SaunaOutputGpioPin { get; set; }
        public int SaunaInputGpioPin { get; set; }
        public int InfraredOutputGpioPin { get; set; }
        public int InfraredInputGpioPin { get; set; }
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
            SaunaHysteresis = ToInt32(configuration, SAUNA_HYSTERESIS);
            InfraredHysteresis = ToInt32(configuration, INFRARED_HYSTERESIS);
            SaunaDefaultDuration = ToInt32(configuration, SAUNA_DEFAULT_DURATION);
            InfraredDefaultDuration = ToInt32(configuration, INFRARED_DEFAULT_DURATION);
            SaunaDefaultTemperature = ToInt32(configuration, SAUNA_DEFAULT_TEMPERATURE);
            InfraredDefaultTemperature = ToInt32(configuration, INFRARED_DEFAULT_TEMPERATURE);
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