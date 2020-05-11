namespace MijnSauna.Common.DataTransferObjects.Logs
{
    public class LogErrorRequest
    {
        public string Component { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}