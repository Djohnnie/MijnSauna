using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MijnSauna.Middleware.Processor
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.Any, 5050,
                            configure => configure.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://192.168.10.2:9200"))
                        {
                            AutoRegisterTemplate = true
                        }).CreateLogger();
                    logging.AddSerilog();
                });
    }
}