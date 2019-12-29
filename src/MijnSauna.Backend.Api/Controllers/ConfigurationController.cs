using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("configuration")]
    [ApiController]
    public class ConfigurationController : ApiController<IConfigurationLogic>
    {
        public ConfigurationController(IConfigurationLogic configurationLogic) : base(configurationLogic) { }

        //[HttpGet]
        //public Task<IActionResult> GetConfigurationValues()
        //{
        //    return Execute(x => x.GetConfigurationValues());
        //}

        //[HttpPost]
        //public Task<IActionResult> CreateConfigurationValue([FromBody] CreateSampleForSessionRequest request)
        //{
        //    return Execute(x => x.CreateSampleForSession(sessionId, request));
        //}
    }
}