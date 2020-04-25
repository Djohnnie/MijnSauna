using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects.Configuration;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("configuration")]
    [ApiController]
    public class ConfigurationController : ApiController<IConfigurationLogic>
    {
        public ConfigurationController(
            IConfigurationLogic configurationLogic,
            ILogger<IConfigurationLogic> logger) : base(configurationLogic, logger) { }

        [HttpGet]
        public Task<IActionResult> GetConfigurationValues()
        {
            return Execute(x => x.GetConfigurationValues());
        }

        [HttpPost]
        public Task<IActionResult> CreateConfigurationValue([FromBody] CreateConfigurationValueRequest request)
        {
            return Execute(x => x.CreateConfigurationValue(request));
        }

        [HttpPut("{name}")]
        public Task<IActionResult> UpdateConfigurationValue(string name, [FromBody] UpdateConfigurationValueRequest request)
        {
            return Execute(x => x.UpdateConfigurationValue(name, request));
        }

        [HttpDelete("{name}")]
        public Task<IActionResult> RemoveConfigurationValue(string name)
        {
            return Execute(x => x.RemoveConfigurationValue(name));
        }
    }
}