using System;
using System.Collections.Generic;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class Session : IHasId
    {
        public Guid Id { get; set; }
        public int SysId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime ActualEnd { get; set; }
        public bool IsSauna { get; set; }
        public bool IsInfrared { get; set; }
        public bool IsCancelled { get; set; }
        public int TemperatureGoal { get; set; }
        public List<Sample> Samples { get; set; }
    }
}