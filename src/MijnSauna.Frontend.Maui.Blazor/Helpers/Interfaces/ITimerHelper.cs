namespace MijnSauna.Frontend.Maui.Blazor.Helpers.Interfaces;

public interface ITimerHelper
{
    ITimer Start(Func<Task> action, int interval);
}

public interface ITimer
{
    void Stop();
}