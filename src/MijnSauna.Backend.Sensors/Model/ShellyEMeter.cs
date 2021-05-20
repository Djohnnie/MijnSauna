using System.Text.Json.Serialization;

namespace MijnSauna.Backend.Sensors.Model
{
    public class ShellyEMeter
    {
        [JsonPropertyName("power")]
        public decimal Power { get; set; }

        [JsonPropertyName("voltage")]
        public decimal Voltage { get; set; }
    }
}