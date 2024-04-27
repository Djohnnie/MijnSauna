namespace MijnSauna.Common.DataTransferObjects.Sensor
{
    public class GetPowerUsageResponse
    {
        public decimal PowerUsage { get; set; }
        public int BatteryPercentage { get; set; }
    }
}