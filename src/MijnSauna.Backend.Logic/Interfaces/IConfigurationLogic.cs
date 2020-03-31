using MijnSauna.Common.DataTransferObjects.Configuration;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Logic.Interfaces
{
    public interface IConfigurationLogic : ILogic
    {
        Task<GetConfigurationValuesResponse> GetConfigurationValues();

        Task<GetConfigurationValueResponse> GetConfigurationValue(string name);

        Task<CreateConfigurationValueResponse> CreateConfigurationValue(CreateConfigurationValueRequest request);
        
        Task<UpdateConfigurationValueResponse> UpdateConfigurationValue(string name, UpdateConfigurationValueRequest request);
        
        Task RemoveConfigurationValue(string name);
    }
}