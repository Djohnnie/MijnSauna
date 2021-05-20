using System.Text.Json.Serialization;

namespace MijnSauna.Backend.Sensors.Model
{
    public class ShellyWifi
    {
        [JsonPropertyName("connected")]
        public bool Connected { get; set; }

        [JsonPropertyName("ssid")]
        public string Ssid { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }
    }
}