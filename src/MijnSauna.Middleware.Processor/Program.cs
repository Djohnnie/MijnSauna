using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

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
                });
    }
}