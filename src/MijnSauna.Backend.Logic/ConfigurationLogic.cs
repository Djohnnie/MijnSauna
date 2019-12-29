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

        public ConfigurationLogic(
            IRepository<ConfigurationValue> configurationRepository,
            IMapper<IList<ConfigurationValue>, GetConfigurationValuesResponse> getConfigurationValuesResponseMapper)
        {
            _configurationRepository = configurationRepository;
            _getConfigurationValuesResponseMapper = getConfigurationValuesResponseMapper;
        }

        public async Task<GetConfigurationValuesResponse> GetConfigurationValues()
        {
            var configurationValues = await _configurationRepository.GetAll();
            var response = _getConfigurationValuesResponseMapper.Map(configurationValues);
            return response;
        }

        public Task<CreateConfigurationValueResponse> CreateConfigurationValue(CreateConfigurationValueRequest request)
        {
            throw new NotImplementedException();
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