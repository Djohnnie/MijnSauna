using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface ILogClient
    {
        Task LogError(LogErrorRequest request);
    }
}