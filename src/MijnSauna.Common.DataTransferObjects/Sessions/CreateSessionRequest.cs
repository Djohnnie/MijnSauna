using System;

namespace MijnSauna.Common.DataTransferObjects.Sessions
{
    public class CreateSessionRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Boolean IsSauna { get; set; }
        public Boolean IsInfrared { get; set; }
        public Decimal TemperatureGoal { get; set; }
    }
}