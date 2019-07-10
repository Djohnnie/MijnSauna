using System;
using System.Collections.Generic;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class Session : IHasId
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? ActualEnd { get; set; }
        public Boolean IsSauna { get; set; }
        public Boolean IsInfrared { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsCancelled { get; set; }
        public Decimal TemperatureGoal { get; set; }
        public List<Sample> Samples { get; set; }
    }
}