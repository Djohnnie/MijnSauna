using System;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class Sample : IHasId
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Decimal Temperature { get; set; }
        public Boolean IsSaunaPowered { get; set; }
        public Boolean IsInfraredPowered { get; set; }
        public Session Session { get; set; }
    }
}