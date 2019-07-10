using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Mappers
{
    public class CreateSampleForSessionResponseMapper : Mapper<Sample, CreateSampleForSessionResponse>
    {
        public CreateSampleForSessionResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sample, CreateSampleForSessionResponse>()
                    .ForMember(d => d.SampleId, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.SessionId, o => o.MapFrom(s => s.Session.Id));
            });
            _mapper = config.CreateMapper();
        }
    }
}