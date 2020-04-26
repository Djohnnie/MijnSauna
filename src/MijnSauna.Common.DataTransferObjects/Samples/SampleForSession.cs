using System;

namespace MijnSauna.Common.DataTransferObjects.Samples
{
    public class SampleForSession
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Temperature { get; set; }
        public bool IsSaunaPowered { get; set; }
        public bool IsInfraredPowered { get; set; }
    }
}