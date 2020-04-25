using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MijnSauna.Backend.Api.Common;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects.Samples;

namespace MijnSauna.Backend.Api.Controllers
{
    [Route("sauna/sessions")]
    [ApiController]
    public class SamplesController : ApiController<ISampleLogic>
    {
        public SamplesController(
            ISampleLogic sampleLogic,
            ILogger<ISampleLogic> logger) : base(sampleLogic, logger) { }

        [HttpGet("{sessionId}/samples")]
        public Task<IActionResult> GetSamplesForSession(Guid sessionId)
        {
            return Execute(x => x.GetSamplesForSession(sessionId));
        }

        [HttpPost("{sessionId}/samples")]
        public Task<IActionResult> CreateSampleForSession(Guid sessionId, [FromBody] CreateSampleForSessionRequest request)
        {
            return Execute(x => x.CreateSampleForSession(sessionId, request));
        }
    }
}