namespace MijnSauna.Common.DataTransferObjects.Sensor
{
    public class GetSaunaPowerUsageResponse
    {
        public decimal SaunaPowerUsage { get; set; }

        public decimal InfraredPowerUsage { get; set; }
    }
}