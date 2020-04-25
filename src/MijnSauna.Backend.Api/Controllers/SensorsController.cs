using Microsoft.AspNetCore.Mvc;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("sensors")]
    [ApiController]
    public class SensorsController : ApiController<ISensorLogic>
    {
        public SensorsController(
            ISensorLogic sensorLogic,
            ILogger<ISensorLogic> logger) : base(sensorLogic, logger) { }

        [HttpGet("power")]
        public Task<IActionResult> GetPowerUsage()
        {
            return Execute(l => l.GetPowerUsage());
        }

        [HttpGet("temperature/sauna")]
        public Task<IActionResult> GetSaunaTemperature()
        {
            return Execute(l => l.GetSaunaTemperature());
        }

        [HttpGet("temperature/outside")]
        public Task<IActionResult> GetOutsideTemperature()
        {
            return Execute(l => l.GetOutsideTemperature());
        }

        [HttpGet("state")]
        public Task<IActionResult> GetSaunaState()
        {
            return Execute(l => l.GetSaunaState());
        }
    }
}