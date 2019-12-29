using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MijnSauna.Backend.Logic.Exceptions;
using MijnSauna.Backend.Logic.Interfaces;
using MijnSauna.Common.DataTransferObjects;

namespace MijnSauna.Backend.Api.Common
{
    public class ApiController : ControllerBase { }

    public class ApiController<TLogic> : ApiController where TLogic : ILogic
    {
        private readonly TLogic _logic;
        private readonly IMemoryCache _memoryCache;

        protected ApiController(TLogic logic)
        {
            _logic = logic;
        }

        protected ApiController(TLogic logic, IMemoryCache memoryCache)
        {
            _logic = logic;
            _memoryCache = memoryCache;
        }

        protected async Task<IActionResult> Execute<TResult>(Func<TLogic, Task<TResult>> logicCall)
        {
            return await Try(async () =>
            {
                var stopwatch = Stopwatch.StartNew();
                if (_memoryCache != null)
                {
                    if (!_memoryCache.TryGetValue(logicCall.Method.Name, out TResult result))
                    {
                        result = await logicCall(_logic);
                        _memoryCache.Set(logicCall.Method.Name, result, TimeSpan.FromSeconds(1));
                    }
                    return result != null ? Ok(result) : (ActionResult)NotFound();
                }
                else
                {
                    TResult result = await logicCall(_logic);
                    return result != null ? ActionResult(200, result, stopwatch.ElapsedMilliseconds) : (ActionResult)NotFound();
                }
            });
        }

        protected async Task<IActionResult> Execute(Func<TLogic, Task> logicCall)
        {
            return await Try(async () =>
            {
                var stopwatch = Stopwatch.StartNew();
                await logicCall(_logic);
                return ActionResult(200, stopwatch.ElapsedMilliseconds);
            });
        }

        protected async Task<IActionResult> Ok(Func<TLogic, Task> logicCall)
        {
            return await Try(async () =>
            {
                await logicCall(_logic);
                return Ok();
            });
        }

        protected async Task<IActionResult> Created<TResult>(Func<TLogic, Task<TResult>> logicCall)
        {
            return await Try(async () =>
            {
                var result = await logicCall(_logic);
                return result != null ? Created("", result) : (ActionResult)NotFound();
            });
        }

        private async Task<IActionResult> Try(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (LogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return ActionResult(500, 0);
            }
        }

        private IActionResult ActionResult<T>(Int32 status, T value, Int64 duration)
        {
            return StatusCode(status, new ApiResult<T> { CorrelationId = Guid.NewGuid(), Duration = duration, Content = value });
        }

        private IActionResult ActionResult(Int32 status, Int64 duration)
        {
            return StatusCode(status, new ApiResult { CorrelationId = Guid.NewGuid(), Duration = duration });
        }
    }
}