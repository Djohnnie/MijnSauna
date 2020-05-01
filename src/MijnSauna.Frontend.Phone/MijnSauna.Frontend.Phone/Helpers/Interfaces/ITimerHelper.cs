using System;
using System.Threading.Tasks;

namespace MijnSauna.Frontend.Phone.Helpers.Interfaces
{
    public interface ITimerHelper
    {
        ITimer Start(Func<Task> action, int interval);
    }

    public interface ITimer
    {
        void Stop();
    }
}