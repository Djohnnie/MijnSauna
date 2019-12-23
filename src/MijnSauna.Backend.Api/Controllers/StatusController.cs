using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MijnSauna.Backend.Api.Common;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("status")]
    [ApiController]
    public class StatusController : ApiController
    {
        [HttpGet]
        public Task<IActionResult> GetSamplesForSession()
        {
            return Task.FromResult((IActionResult)Ok());
        }
    }
}