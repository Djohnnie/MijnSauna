using Microsoft.AspNetCore.Http;
using MijnSauna.Backend.Common.Interfaces;
using System.Threading.Tasks;

namespace MijnSauna.Backend.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(
            IConfigurationHelper configurationHelper,
            RequestDelegate next)
        {
            _configurationHelper = configurationHelper;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("swagger"))
            {
                await _next.Invoke(context);
                return;
            }

            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("metrics"))
            {
                await _next.Invoke(context);
                return;
            }

            string authHeader = context.Request.Headers["ClientId"];
            if (authHeader != null)
            {
                if (authHeader == _configurationHelper.ClientId)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}