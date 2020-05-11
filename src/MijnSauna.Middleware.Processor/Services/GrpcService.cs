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

            await _gpioService.Initialize();
            var temperature = await _gpioService.ReadTemperature();

            return new GetTemperatureResponse
            {
                Temperature = temperature
            };
        }

        public override async Task<GetStateResponse> GetState(GetStateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("State requested by gRPC!");

            await _gpioService.Initialize();
            var isSaunaOn = await _gpioService.IsSaunaOn();
            var isInfraredOn = await _gpioService.IsInfraredOn();

            return new GetStateResponse
            {
                IsSaunaOn = isSaunaOn,
                IsInfraredOn = isInfraredOn
            };
        }
    }
}