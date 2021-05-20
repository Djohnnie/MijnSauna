using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MijnSauna.Backend.Sensors.Model
{
    public class ShellyEMetering
    {
        [JsonPropertyName("wifi_sta")]
        public ShellyWifi Wifi { get; set; }

        [JsonPropertyName("emeters")]
        public List<ShellyEMeter> Meters { get; set; }
    }
}