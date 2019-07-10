using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Mappers
{
    public class CreateSessionResponseMapper : Mapper<Session, CreateSessionResponse>
    {
        public CreateSessionResponseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Session, CreateSessionResponse>()
                    .ForMember(d => d.SessionId, o => o.MapFrom(s => s.Id));
            });
            _mapper = config.CreateMapper();
        }
    }
}