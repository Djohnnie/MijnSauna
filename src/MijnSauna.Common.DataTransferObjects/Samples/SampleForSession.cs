using System;

namespace MijnSauna.Common.DataTransferObjects.Samples
{
    public class SampleForSession
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public Decimal Temperature { get; set; }
        public Boolean IsSaunaPowered { get; set; }
        public Boolean IsInfraredPowered { get; set; }
    }
}