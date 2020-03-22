using System.Threading.Tasks;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface IServiceClient
    {
        Task<TResponse> Get<TResponse>(string resource);
    }
}