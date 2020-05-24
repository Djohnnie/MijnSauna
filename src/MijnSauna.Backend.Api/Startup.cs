using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MijnSauna.Backend.Api.DependencyInjection;
using MijnSauna.Backend.Api.Middleware;
using MijnSauna.Backend.Api.Swagger;
using Prometheus;
using static System.Environment;

namespace MijnSauna.Backend.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureApi(c =>
            {
                c.ConnectionString = GetEnvironmentVariable("CONNECTIONSTRING");
                c.ClientId = GetEnvironmentVariable("CLIENT_ID");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MijnSauna API", Version = "v1" });
                c.OperationFilter<ClientIdHeaderParameterOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MijnSauna API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
        }
    }
}