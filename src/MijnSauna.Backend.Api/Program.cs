﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MijnSauna.Backend.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var certificateFileName = Environment.GetEnvironmentVariable("CERTIFICATE_FILENAME");
            var certificatePassword = Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD");
            CreateHostBuilder(args, certificateFileName, certificatePassword).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string certificateFileName, string certificatePassword) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.Any, 5000);
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}