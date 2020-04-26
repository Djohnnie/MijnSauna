using System;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class Sample : IHasId
    {
        public Guid Id { get; set; }
        public int SysId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Temperature { get; set; }
        public bool IsSaunaPowered { get; set; }
        public bool IsInfraredPowered { get; set; }
        public Session Session { get; set; }
    }
}