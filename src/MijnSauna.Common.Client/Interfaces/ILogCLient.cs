using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface ILogClient
    {
        Task LogInformation(LogInformationRequest request);

        Task LogError(LogErrorRequest request);
    }
}