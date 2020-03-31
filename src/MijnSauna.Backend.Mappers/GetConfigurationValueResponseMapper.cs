using AutoMapper;
using MijnSauna.Common.DataTransferObjects.Configuration;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;

namespace MijnSauna.Backend.Mappers
{
    public class GetConfigurationValueResponseMapper : Mapper<ConfigurationValue, GetConfigurationValueResponse>
    {
        public GetConfigurationValueResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationValue, GetConfigurationValueResponse>();
            });
            _mapper = config.CreateMapper();
        }
    }
}