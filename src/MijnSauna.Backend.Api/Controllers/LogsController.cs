using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects.Logs;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("logs")]
    [ApiController]
    public class LogsController : ApiController<ILogLogic>
    {
        public LogsController(
            ILogLogic logLogic,
            ILogger<ILogLogic> logger) : base(logLogic, logger) { }

        [HttpPost("info")]
        public Task<IActionResult> LogInformation([FromBody] LogInformationRequest request)
        {
            return Execute(l => l.LogInformation(request));
        }

        [HttpPost("error")]
        public Task<IActionResult> LogError([FromBody] LogErrorRequest request)
        {
            return Execute(l => l.LogError(request));
        }
    }
}