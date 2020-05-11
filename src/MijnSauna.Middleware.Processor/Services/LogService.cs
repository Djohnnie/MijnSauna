using System;
using System.Text;
using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Logs;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class LogService : ILogService
    {
        private readonly ILogClient _logClient;

        public LogService(
            ILogClient logClient)
        {
            _logClient = logClient;
        }

        public Task LogInformation(string title, string message)
        {
            return _logClient.LogInformation(new LogInformationRequest
            {
                Title = title,
                Message = message,
                Component = "MijnSauna.Middleware.Processor"
            });
        }

        public Task LogException(string title, string message, Exception ex)
        {
            return _logClient.LogError(new LogErrorRequest
            {
                Title = title,
                Message = message,
                Component = "MijnSauna.Middleware.Processor",
                ExceptionMessage = GetExceptionMessage(ex),
                ExceptionType = $"{ex.GetType()}",
                ExceptionStackTrace = ex.StackTrace
            });
        }

        private string GetExceptionMessage(Exception ex)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append(ex.Message);

            if (ex.InnerException != null)
            {
                messageBuilder.Append(" - ");
                messageBuilder.Append(GetExceptionMessage(ex.InnerException));
            }

            return messageBuilder.ToString();
        }
    }
}