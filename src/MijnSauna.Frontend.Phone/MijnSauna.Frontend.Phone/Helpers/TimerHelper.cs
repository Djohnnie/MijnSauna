using System;
using System.Threading;
using System.Threading.Tasks;
using MijnSauna.Frontend.Phone.Helpers.Interfaces;

namespace MijnSauna.Frontend.Phone.Helpers
{
    public class TimerHelper : ITimerHelper
    {
        public ITimer Start(Func<Task> action, int interval)
        {
            var timer = new Timer();
            timer.Start(action, interval);
            return timer;
        }

        public class Timer : ITimer
        {
            private CancellationTokenSource _cancellation;
            private Task _backgroundTask;

            public void Start(Func<Task> action, int interval)
            {
                if (_backgroundTask == null)
                {
                    _cancellation = new CancellationTokenSource();
                    _backgroundTask = Task.Run(async () =>
                    {
                        while (!_cancellation.IsCancellationRequested)
                        {
                            try
                            {
                                await action();
                                await Task.Delay(interval, _cancellation.Token);
                            }
                            catch
                            {
                                // Do nothing!
                            }
                        }
                    });
                }
            }

            public void Stop()
            {
                _cancellation.Cancel();
                _backgroundTask.Dispose();
                _backgroundTask = null;
                _cancellation.Dispose();
                _cancellation = null;
            }
        }
    }
}