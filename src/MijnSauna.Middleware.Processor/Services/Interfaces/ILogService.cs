using System;
using System.Threading.Tasks;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface ILogService
    {
        Task LogInformation(string title, string message);

        Task LogException(string title, string message, Exception ex);
    }
}