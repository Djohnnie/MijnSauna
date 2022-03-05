using System.Threading.Tasks;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface IServiceClient
    {
        Task<TResponse> Get<TResponse>(string resource) where TResponse : new();

        Task Post<TBody>(string resource, TBody body) where TBody : class;

        Task<TResponse> Post<TResponse, TBody>(string resource, TBody body) where TResponse : new() where TBody : class;

        Task Put(string resource);
    }
}