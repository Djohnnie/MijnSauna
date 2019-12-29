using System.Collections.Generic;
using AutoMapper;
using MijnSauna.Common.DataTransferObjects.Configuration;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;
using ConfigurationValueDto = MijnSauna.Common.DataTransferObjects.Configuration.ConfigurationValue;

namespace MijnSauna.Backend.Mappers
{
    public class GetConfigurationValuesResponseMapper : Mapper<IList<ConfigurationValue>, GetConfigurationValuesResponse>
    {
        public GetConfigurationValuesResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationValue, ConfigurationValueDto>();
                cfg.CreateMap<IList<ConfigurationValue>, GetConfigurationValuesResponse>()
                    .ForMember(d => d.ConfigurationValues, o => o.MapFrom(s => s));
            });
            _mapper = config.CreateMapper();
        }
    }
}