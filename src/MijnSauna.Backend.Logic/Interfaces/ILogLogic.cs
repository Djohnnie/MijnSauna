using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Backend.Logic.Interfaces
{
    public interface ILogLogic : ILogic
    {
        Task LogInformation(LogInformationRequest request);

        Task LogError(LogErrorRequest request);
    }
}