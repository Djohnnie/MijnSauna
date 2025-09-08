using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MijnSauna.Backend.Api.DependencyInjection;
using MijnSauna.Backend.Api.Middleware;
using MijnSauna.Backend.Api.Swagger;
using MijnSauna.Backend.Api.Tools;
using Prometheus;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    var certificateFileName = context.Configuration.GetValue<string>("CERTIFICATE_FILENAME");
    var certificatePassword = context.Configuration.GetValue<string>("CERTIFICATE_PASSWORD");

    if (string.IsNullOrEmpty(certificateFileName) || string.IsNullOrEmpty(certificatePassword))
    {
        options.Listen(IPAddress.Any, 5000);
    }
    else
    {
        options.Listen(IPAddress.Any, 5000,
            listenOptions => { listenOptions.UseHttps(certificateFileName, certificatePassword); });
    }
});

builder.Services.AddControllers();

builder.Services.ConfigureApi(c =>
{
    c.ConnectionString = builder.Configuration.GetValue<string>("CONNECTIONSTRING");
    c.ClientId = builder.Configuration.GetValue<string>("CLIENT_ID");
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MijnSauna API", Version = "v1" });
    c.OperationFilter<ClientIdHeaderParameterOperationFilter>();
});

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<MijnSaunaTools>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.MapMcp("mcp");

app.Run();