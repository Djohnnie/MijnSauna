using System;
using System.Threading.Tasks;

namespace MijnSauna.Frontend.Phone.Helpers.Interfaces
{
    public interface ITimerHelper
    {
        void Start(Func<Task> action, int interval);

        void Stop();
    }
}