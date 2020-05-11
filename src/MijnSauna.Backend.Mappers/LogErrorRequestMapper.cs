using System;
using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Backend.Mappers
{
    public class LogErrorRequestMapper : Mapper<Log, LogErrorRequest>
    {
        public LogErrorRequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LogErrorRequest, Log>()
                    .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => DateTime.UtcNow))
                    .ForMember(dest => dest.IsError, opt => opt.MapFrom(src => true));
            });
            _mapper = config.CreateMapper();
        }
    }
}