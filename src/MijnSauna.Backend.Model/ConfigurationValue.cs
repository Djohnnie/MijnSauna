using System;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class ConfigurationValue : IHasId
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
    }
}