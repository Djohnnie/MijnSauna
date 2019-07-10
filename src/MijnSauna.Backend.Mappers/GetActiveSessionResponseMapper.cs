using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Mappers
{
    public class GetActiveSessionResponseMapper : Mapper<Session, GetActiveSessionResponse>
    {
        public GetActiveSessionResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Session, GetActiveSessionResponse>()
                    .ForMember(d => d.SessionId, o => o.MapFrom(s => s.Id));
            });
            _mapper = config.CreateMapper();
        }
    }
}