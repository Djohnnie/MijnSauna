using System.Threading.Tasks;
using MijnSauna.Common.DataTransferObjects.Configuration;

namespace MijnSauna.Common.Client.Interfaces
{
    public interface IConfigurationClient
    {
        Task<GetConfigurationValuesResponse> GetConfigurationValues();
    }
}