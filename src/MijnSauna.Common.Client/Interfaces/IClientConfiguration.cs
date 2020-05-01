namespace MijnSauna.Common.Client.Interfaces
{
    public interface IClientConfiguration
    {
        string ServiceBaseUrl { get; set; }

        string ClientId { get; set; }

        bool IsSaunaMode { get; set; }
    }
}