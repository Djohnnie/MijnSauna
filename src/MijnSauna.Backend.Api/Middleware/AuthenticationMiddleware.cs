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
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}