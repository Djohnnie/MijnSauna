using AutoMapper;
using MijnSauna.Common.DataTransferObjects.Configuration;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;

namespace MijnSauna.Backend.Mappers
{
    public class CreateConfigurationValueResponseMapper : Mapper<ConfigurationValue, CreateConfigurationValueResponse>
    {
        public CreateConfigurationValueResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationValue, CreateConfigurationValueResponse>();
            });
            _mapper = config.CreateMapper();
        }
    }
}