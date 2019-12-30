using System.Threading.Tasks;

namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface IGpioService
    {
        void Initialize();

        void TurnSaunaOn();

        void TurnSaunaOff();

        bool IsSaunaOn();

        void TurnInfraredOn();

        void TurnInfraredOff();

        bool IsInfraredOn();

        Task<int> ReadTemperature();

        void Shutdown();
    }
}