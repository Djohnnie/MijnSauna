using System.Threading.Tasks;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Backend.Logic
{
    public class LogLogic : ILogLogic
    {
        private readonly IRepository<Log> _logRepository;
        private readonly IMapper<Log, LogInformationRequest> _logInformationRequestMapper;
        private readonly IMapper<Log, LogErrorRequest> _logErrorRequestMapper;

        public LogLogic(
            IRepository<Log> logRepository,
            IMapper<Log, LogInformationRequest> logInformationRequestMapper,
            IMapper<Log, LogErrorRequest> logErrorRequestMapper)
        {
            _logRepository = logRepository;
            _logInformationRequestMapper = logInformationRequestMapper;
            _logErrorRequestMapper = logErrorRequestMapper;
        }

        public async Task LogInformation(LogInformationRequest request)
        {
            var log = _logInformationRequestMapper.Map(request);
            await _logRepository.Create(log);
        }

        public async Task LogError(LogErrorRequest request)
        {
            var log = _logErrorRequestMapper.Map(request);
            await _logRepository.Create(log);
        }
    }
}