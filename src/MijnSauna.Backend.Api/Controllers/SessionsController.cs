using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects.Sessions;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("sauna/sessions")]
    [ApiController]
    public class SessionsController : ApiController<ISessionLogic>
    {
        public SessionsController(
            ISessionLogic sessionLogic,
            ILogger<ISessionLogic> logger) : base(sessionLogic, logger) { }

        [HttpGet("active")]
        public Task<IActionResult> GetActiveSession()
        {
            return Execute(l => l.GetActiveSession());
        }

        [HttpGet]
        public async Task<IActionResult> GetPastSessions()
        {
            await Task.Delay(1);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(Guid id)
        {
            await Task.Delay(1);
            return Ok();
        }

        [HttpPost]
        public Task<IActionResult> CreateSession([FromBody] CreateSessionRequest request)
        {
            return Execute(l => l.CreateSession(request));
        }

        [HttpPost("quickstart")]
        public Task<IActionResult> QuickStartSession([FromBody] QuickStartSessionRequest request)
        {
            return Execute(l => l.QuickStartSession(request));
        }

        [HttpPut("{id}/cancel")]
        public Task<IActionResult> CancelSession(Guid id)
        {
            return Execute(l => l.CancelSession(id));
        }
    }
}