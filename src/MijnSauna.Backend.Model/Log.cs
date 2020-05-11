using System;
using MijnSauna.Backend.Model.Interfaces;

namespace MijnSauna.Backend.Model
{
    public class Log : IHasId
    {
        public Guid Id { get; set; }
        public int SysId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Component { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}