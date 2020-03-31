using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Backend.Mappers.Interfaces;
using MijnSauna.Common.DataTransferObjects.Configuration;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;

namespace MijnSauna.Backend.Logic
{
    public class ConfigurationLogic : IConfigurationLogic
    {
        private readonly IRepository<ConfigurationValue> _configurationRepository;
        private readonly IMapper<IList<ConfigurationValue>, GetConfigurationValuesResponse> _getConfigurationValuesResponseMapper;
        private readonly IMapper<ConfigurationValue, GetConfigurationValueResponse> _getConfigurationValueResponseMapper;
        private readonly IMapper<CreateConfigurationValueRequest, ConfigurationValue> _createConfigurationValueRequestMapper;
        private readonly IMapper<ConfigurationValue, CreateConfigurationValueResponse> _createConfigurationValueResponseMapper;

        public ConfigurationLogic(
            IRepository<ConfigurationValue> configurationRepository,
            IMapper<IList<ConfigurationValue>, GetConfigurationValuesResponse> getConfigurationValuesResponseMapper,
            IMapper<ConfigurationValue, GetConfigurationValueResponse> getConfigurationValueResponseMapper,
            IMapper<CreateConfigurationValueRequest, ConfigurationValue> createConfigurationValueRequestMapper,
            IMapper<ConfigurationValue, CreateConfigurationValueResponse> createConfigurationValueResponseMapper)
        {
            _configurationRepository = configurationRepository;
            _getConfigurationValuesResponseMapper = getConfigurationValuesResponseMapper;
            _getConfigurationValueResponseMapper = getConfigurationValueResponseMapper;
            _createConfigurationValueRequestMapper = createConfigurationValueRequestMapper;
            _createConfigurationValueResponseMapper = createConfigurationValueResponseMapper;
        }

        public async Task<GetConfigurationValuesResponse> GetConfigurationValues()
        {
            var configurationValues = await _configurationRepository.GetAll();
            var response = _getConfigurationValuesResponseMapper.Map(configurationValues);
            return response;
        }

        public async Task<GetConfigurationValueResponse> GetConfigurationValue(string name)
        {
            var configurationValue = await _configurationRepository.Single(x => x.Name == name);
            var response = _getConfigurationValueResponseMapper.Map(configurationValue);
            return response;
        }

        public async Task<CreateConfigurationValueResponse> CreateConfigurationValue(CreateConfigurationValueRequest request)
        {
            var configurationValue = _createConfigurationValueRequestMapper.Map(request);
            configurationValue = await _configurationRepository.Create(configurationValue);
            var response = _createConfigurationValueResponseMapper.Map(configurationValue);
            return response;
        }

        public Task<UpdateConfigurationValueResponse> UpdateConfigurationValue(string name, UpdateConfigurationValueRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveConfigurationValue(string name)
        {
            var configurationValue = await _configurationRepository.Single(x => x.Name == name);
            if (configurationValue != null)
            {
                await _configurationRepository.Remove(configurationValue);
            }
        }
    }
}