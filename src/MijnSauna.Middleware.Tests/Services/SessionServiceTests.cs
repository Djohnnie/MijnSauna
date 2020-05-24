using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Sessions;
using MijnSauna.Middleware.Processor.Context.Interfaces;
using MijnSauna.Middleware.Processor.Services;
using MijnSauna.Middleware.Processor.Services.Interfaces;
using Moq;
using Xunit;

namespace MijnSauna.Middleware.Tests.Services
{
    public class SessionServiceTests
    {
        [Fact]
        public async Task SessionService_UpdateSession_Without_IsSauna_Or_IsInfrared_Should_Not_Do_Anything()
        {
            // Arrange
            var sessionContext = new Mock<ISessionContext>();
            var gpioService = new Mock<IGpioService>();
            var loggerService = new Mock<ILoggerService<SessionService>>();
            var sessionService = new SessionService(sessionContext.Object, gpioService.Object, loggerService.Object);

            var data = new GetActiveSessionResponse
            {
                IsSauna = false,
                IsInfrared = false
            };

            // Mock
            gpioService.Setup(x => x.ReadTemperature()).ReturnsAsync(0);
            gpioService.Setup(x => x.IsSaunaOn()).ReturnsAsync(false);
            gpioService.Setup(x => x.IsInfraredOn()).ReturnsAsync(false);

            // Act
            await sessionService.UpdateSession(data);

            // Assert
            gpioService.Verify(x => x.TurnSaunaOn(), Times.Never);
            gpioService.Verify(x => x.TurnSaunaOff(), Times.Never);
            gpioService.Verify(x => x.TurnInfraredOn(), Times.Never);
            gpioService.Verify(x => x.TurnInfraredOff(), Times.Never);
        }
    }
}