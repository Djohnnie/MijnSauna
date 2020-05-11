using System.Threading.Tasks;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface IGpioService
    {
        Task Initialize();

        Task TurnSaunaOn();

        Task TurnSaunaOff();

        Task<bool> IsSaunaOn();

        Task TurnInfraredOn();

        Task TurnInfraredOff();

        Task<bool> IsInfraredOn();

        Task<int> ReadTemperature();

        Task Shutdown();
    }
}