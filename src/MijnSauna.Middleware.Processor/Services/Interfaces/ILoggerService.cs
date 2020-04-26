namespace MijnSauna.Middleware.Processor.Services.Interfaces
{
    public interface ILoggerService<out TCategoryName>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}