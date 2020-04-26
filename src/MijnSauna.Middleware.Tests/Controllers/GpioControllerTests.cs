using System.Threading.Tasks;
using Xunit;
using MijnSauna.Middleware.Processor.Controllers;

namespace MijnSauna.Middleware.Tests.Controllers
{
    public class GpioControllerTests
    {
        [Fact]
        public async Task Repository_GetAll_Should_Return_All_Records()
        {
            // Act
            var gpioController = new GpioController();

            // Assert
        }
    }
}