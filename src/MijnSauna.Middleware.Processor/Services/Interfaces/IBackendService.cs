using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface IBackendService
    {
        Task<GetActiveSessionResponse> GetActiveSession();
    }
}