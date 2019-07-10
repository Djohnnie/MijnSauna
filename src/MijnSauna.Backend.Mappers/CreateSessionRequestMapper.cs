using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Mappers
{
    public class CreateSessionRequestMapper : Mapper<Session, CreateSessionRequest>
    {
        public CreateSessionRequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateSessionRequest, Session>();
            });
            _mapper = config.CreateMapper();
        }
    }
}