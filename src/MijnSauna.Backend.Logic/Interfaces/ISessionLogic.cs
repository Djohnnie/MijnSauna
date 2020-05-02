using System;
using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Logic.Interfaces
{
    public interface ISessionLogic : ILogic
    {
        Task<GetActiveSessionResponse> GetActiveSession();

        Task<CreateSessionResponse> CreateSession(CreateSessionRequest request);

        Task CancelSession(Guid sessionId);

        Task<CreateSessionResponse> QuickStartSession(QuickStartSessionRequest request);
    }
}