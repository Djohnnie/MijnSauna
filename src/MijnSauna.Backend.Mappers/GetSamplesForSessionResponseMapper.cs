using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Mappers
{
    public class GetSamplesForSessionResponseMapper : Mapper<IList<Sample>, GetSamplesForSessionResponse>
    {
        public GetSamplesForSessionResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sample, SampleForSession>();
                cfg.CreateMap<IList<Sample>, GetSamplesForSessionResponse>()
                    .ForMember(d => d.Samples, o => o.MapFrom(s => s));
            });
            _mapper = config.CreateMapper();
        }
    }
}