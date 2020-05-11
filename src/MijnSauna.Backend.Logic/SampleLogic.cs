using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Logic
{
    public class SampleLogic : ISampleLogic
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Sample> _sampleRepository;
        private readonly IMapper<IList<Sample>, GetSamplesForSessionResponse> _getSamplesForSessionResponseMapper;
        private readonly IMapper<Sample, CreateSampleForSessionRequest> _createSampleForSessionRequestMapper;
        private readonly IMapper<Sample, CreateSampleForSessionResponse> _createSampleForSessionResponseMapper;

        public SampleLogic(
            IRepository<Session> sessionRepository,
            IRepository<Sample> sampleRepository,
            IMapper<IList<Sample>, GetSamplesForSessionResponse> getSamplesForSessionResponseMapper,
            IMapper<Sample, CreateSampleForSessionRequest> createSampleForSessionRequestMapper,
            IMapper<Sample, CreateSampleForSessionResponse> createSampleForSessionResponseMapper)
        {
            _sessionRepository = sessionRepository;
            _sampleRepository = sampleRepository;
            _getSamplesForSessionResponseMapper = getSamplesForSessionResponseMapper;
            _createSampleForSessionRequestMapper = createSampleForSessionRequestMapper;
            _createSampleForSessionResponseMapper = createSampleForSessionResponseMapper;
        }

        public async Task<GetSamplesForSessionResponse> GetSamplesForSession(Guid sessionId)
        {
            var samples = await _sampleRepository.Find(x => x.Session.Id == sessionId);
            var response = _getSamplesForSessionResponseMapper.Map(samples);
            response.SessionId = sessionId;
            return response;
        }

        public async Task<CreateSampleForSessionResponse> CreateSampleForSession(Guid sessionId, CreateSampleForSessionRequest request)
        {
            var session = await _sessionRepository.Single(x => x.Id == sessionId);
            if (session == null) { return null; }

            var sampleToCreate = _createSampleForSessionRequestMapper.Map(request);
            sampleToCreate.Session = session;
            var createdSample = await _sampleRepository.Create(sampleToCreate);
            return _createSampleForSessionResponseMapper.Map(createdSample);
        }
    }
}