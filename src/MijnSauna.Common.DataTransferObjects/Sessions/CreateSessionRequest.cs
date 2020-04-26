using System;

namespace MijnSauna.Common.DataTransferObjects.Sessions
{
    public class CreateSessionRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsSauna { get; set; }
        public bool IsInfrared { get; set; }
        public int TemperatureGoal { get; set; }
    }
}