using System;
using System.Threading.Tasks;
using MijnSauna.Backend.Common.Constants;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Logic.Validation.Interfaces;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Backend.Model;
using MijnSauna.Backend.Sensors.Configuration;
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
        private readonly IConfigurationProxy _configurationProxy;

        public SessionLogic(
            IRepository<Session> sessionRepository,
            IMapper<Session, GetActiveSessionResponse> getActiveSessionResponseMapper,
            IMapper<Session, CreateSessionRequest> createSessionRequestMapper,
            IMapper<Session, CreateSessionResponse> createSessionResponseMapper,
            IValidator<CreateSessionRequest> createSessionRequestValidator,
            IConfigurationProxy configurationProxy)
        {
            _sessionRepository = sessionRepository;
            _getActiveSessionResponseMapper = getActiveSessionResponseMapper;
            _createSessionRequestMapper = createSessionRequestMapper;
            _createSessionResponseMapper = createSessionResponseMapper;
            _createSessionRequestValidator = createSessionRequestValidator;
            _configurationProxy = configurationProxy;
        }

        public async Task<GetActiveSessionResponse> GetActiveSession()
        {
            var activeSession = await _sessionRepository.Single(x => x.Start <= DateTime.UtcNow && x.ActualEnd >= DateTime.UtcNow);
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
            if (session != null)
            {
                session.ActualEnd = DateTime.UtcNow;
                await _sessionRepository.Update(session);
            }
        }

        public async Task<CreateSessionResponse> QuickStartSession(QuickStartSessionRequest request)
        {
            var defaultTemperature = await _configurationProxy.GetInt32(
                request.IsSauna ? ConfigurationConstants.SAUNA_DEFAULT_TEMPERATURE : ConfigurationConstants.INFRARED_DEFAULT_TEMPERATURE);
            var defaultDuration = await _configurationProxy.GetInt32(
                request.IsSauna ? ConfigurationConstants.SAUNA_DEFAULT_DURATION : ConfigurationConstants.INFRARED_DEFAULT_DURATION);

            var start = DateTime.UtcNow;
            var end = start.AddMinutes(defaultDuration);

            var session = new Session
            {
                IsSauna = request.IsSauna,
                IsInfrared = request.IsInfrared,
                Start = start,
                End = end,
                ActualEnd = end,
                TemperatureGoal = defaultTemperature
            };

            session = await _sessionRepository.Create(session);
            return _createSessionResponseMapper.Map(session);
        }
    }
}