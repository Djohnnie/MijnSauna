using System;
using System.Threading.Tasks;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface ILogService
    {
        Task LogException(string title, string message, Exception ex);
    }
}