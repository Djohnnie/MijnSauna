using System;
using AutoMapper;
using MijnSauna.Backend.Model;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Backend.Mappers
{
    public class LogInformationRequestMapper : Mapper<Log, LogInformationRequest>
    {
        public LogInformationRequestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LogInformationRequest, Log>()
                    .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => DateTime.UtcNow))
                    .ForMember(dest => dest.IsError, opt => opt.MapFrom(src => false));
            });
            _mapper = config.CreateMapper();
        }
    }
}