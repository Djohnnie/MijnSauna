using System;

namespace MijnSauna.Common.DataTransferObjects.Sessions
{
    public class GetActiveSessionResponse
    {
        public Guid SessionId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsSauna { get; set; }
        public bool IsInfrared { get; set; }
        public int TemperatureGoal { get; set; }
    }
}