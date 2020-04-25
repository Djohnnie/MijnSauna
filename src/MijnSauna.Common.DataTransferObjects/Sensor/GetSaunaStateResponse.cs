namespace MijnSauna.Common.DataTransferObjects.Sensor
{
    public class GetSaunaStateResponse
    {
        public bool IsSaunaOn { get; set; }
        public bool IsInfraredOn { get; set; }
    }
}