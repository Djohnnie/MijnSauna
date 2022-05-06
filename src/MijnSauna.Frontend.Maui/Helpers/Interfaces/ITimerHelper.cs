namespace MijnSauna.Frontend.Maui.Helpers.Interfaces;

public interface ITimerHelper
{
    ITimer Start(Func<Task> action, int interval);
}

public interface ITimer
{
    void Stop();
}