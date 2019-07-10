using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Mappers
{
    public class CreateSampleForSessionRequestMapper : Mapper<Sample, CreateSampleForSessionRequest>
    {
        public CreateSampleForSessionRequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateSampleForSessionRequest, Sample>();
            });
            _mapper = config.CreateMapper();
        }
    }
}