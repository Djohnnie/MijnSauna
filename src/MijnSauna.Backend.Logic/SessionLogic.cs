using System;
using System.Threading.Tasks;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Logic.Validation.Interfaces;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Logic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IMapper<Session, GetActiveSessionResponse> _getActiveSessionResponseMapper;
        private readonly IMapper<Session, CreateSessionRequest> _createSessionRequestMapper;
        private readonly IMapper<Session, CreateSessionResponse> _createSessionResponseMapper;
        private readonly IValidator<CreateSessionRequest> _createSessionRequestValidator;

        public SessionLogic(
            IRepository<Session> sessionRepository,
            IMapper<Session, GetActiveSessionResponse> getActiveSessionResponseMapper,
            IMapper<Session, CreateSessionRequest> createSessionRequestMapper,
            IMapper<Session, CreateSessionResponse> createSessionResponseMapper,
            IValidator<CreateSessionRequest> createSessionRequestValidator)
        {
            _sessionRepository = sessionRepository;
            _getActiveSessionResponseMapper = getActiveSessionResponseMapper;
            _createSessionRequestMapper = createSessionRequestMapper;
            _createSessionResponseMapper = createSessionResponseMapper;
            _createSessionRequestValidator = createSessionRequestValidator;
        }

        public async Task<GetActiveSessionResponse> GetActiveSession()
        {
            var activeSession = await _sessionRepository.Single(x => x.Start <= DateTime.UtcNow && x.End >= DateTime.UtcNow);
            if (activeSession.ActualEnd.HasValue && activeSession.ActualEnd.Value <= DateTime.UtcNow)
            {
                return null;
            }

            return _getActiveSessionResponseMapper.Map(activeSession);
        }

        public async Task<CreateSessionResponse> CreateSession(CreateSessionRequest request)
        {
            _createSessionRequestValidator.Validate(request);

            var session = _createSessionRequestMapper.Map(request);
            session = await _sessionRepository.Create(session);
            return _createSessionResponseMapper.Map(session);
        }

        public async Task CancelSession(Guid sessionId)
        {
            var session = await _sessionRepository.Single(x => x.Id == sessionId);
            session.ActualEnd = DateTime.UtcNow;
            await _sessionRepository.Update(session);
        }
    }
}