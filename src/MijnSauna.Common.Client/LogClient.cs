using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Common.Client
{
    public class LogClient : ILogClient
    {
        private readonly IServiceClient _serviceClient;

        public LogClient(
            IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Task LogInformation(LogInformationRequest request)
        {
            return _serviceClient.Post("logs/info", request);
        }

        public Task LogError(LogErrorRequest request)
        {
            return _serviceClient.Post("logs/error", request);
        }
    }
}