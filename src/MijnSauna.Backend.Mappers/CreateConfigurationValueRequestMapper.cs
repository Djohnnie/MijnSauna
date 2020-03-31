using AutoMapper;
using MijnSauna.Common.DataTransferObjects.Configuration;
using ConfigurationValue = MijnSauna.Backend.Model.ConfigurationValue;

namespace MijnSauna.Backend.Mappers
{
    public class CreateConfigurationValueRequestMapper : Mapper<CreateConfigurationValueRequest, ConfigurationValue>
    {
        public CreateConfigurationValueRequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateConfigurationValueRequest, ConfigurationValue>();
            });
            _mapper = config.CreateMapper();
        }
    }
}