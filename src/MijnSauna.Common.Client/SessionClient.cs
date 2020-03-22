using System.Threading.Tasks;
using MijnSauna.Common.Client.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Common.Client
{
    public class SessionClient : ISessionClient
    {
        private readonly IServiceClient _serviceClient;

        public SessionClient(
            IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Task<GetActiveSessionResponse> GetActiveSession()
        {
            return _serviceClient.Get<GetActiveSessionResponse>("sauna/sessions/active");
        }

        public Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}