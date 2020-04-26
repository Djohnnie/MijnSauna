using System.Threading.Tasks;
using Grpc.Core;
using MijnSauna.Common.Protobuf;
using MijnSauna.Middleware.Processor.Services.Interfaces;

namespace MijnSauna.Middleware.Processor.Services
{
    public class GrpcService : SaunaService.SaunaServiceBase
    {
        private readonly IGpioService _gpioService;
        private readonly ILoggerService<GrpcService> _logger;

        public GrpcService(
            IGpioService gpioService,
            ILoggerService<GrpcService> logger)
        {
            _gpioService = gpioService;
            _logger = logger;
        }

        public override async Task<GetTemperatureResponse> GetTemperature(GetTemperatureRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Temperature requested by gRPC!");

            _gpioService.Initialize();
            var temperature = await _gpioService.ReadTemperature();

            return new GetTemperatureResponse
            {
                Temperature = temperature
            };
        }

        public override Task<GetStateResponse> GetState(GetStateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("State requested by gRPC!");

            _gpioService.Initialize();
            var isSaunaOn  = _gpioService.IsSaunaOn();
            var isInfraredOn = _gpioService.IsInfraredOn();

            return Task.FromResult(new GetStateResponse
            {
                IsSaunaOn = isSaunaOn,
                IsInfraredOn = isInfraredOn
            });
        }
    }
}