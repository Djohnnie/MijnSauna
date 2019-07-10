using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MijnSauna.Backend.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var certificateFileName = Environment.GetEnvironmentVariable("CERTIFICATE_FILENAME");
            var certificatePassword = Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD");
            CreateWebHostBuilder(args, certificateFileName, certificatePassword).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(String[] args, String certificateFileName, String certificatePassword) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .ConfigureKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.Any, 5000);
                        options.Listen(IPAddress.Any, 5001, listenOptions =>
                        {
                            listenOptions.UseHttps(certificateFileName, certificatePassword);
                        });
                    })
                    .UseStartup<Startup>();
    }
}