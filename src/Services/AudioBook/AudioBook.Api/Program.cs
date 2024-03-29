﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace AudioBook.Api
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            ////var builder = new ConfigurationBuilder()
            ////                .SetBasePath(Directory.GetCurrentDirectory())
            ////                .AddJsonFile("appsettings.json");
            ////Configuration = builder.AddCommandLine(args).Build();
            ////var url = $"{Configuration["Domain:Url"]}:{Configuration["Domain:Port"]}";

            const string PORT = "5000";
            var listeningUrl = $"http://localhost:{PORT}";

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(x => x.AddConsole())
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                        config.AddCommandLine(args);
                    })
                    .UseStartup<Startup>()
                    //.UseUrls(url)
                    .UseSerilog((ctx, cfg) =>
                    {
                        cfg.ReadFrom.Configuration(ctx.Configuration)
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.RollingFile(
                            new JsonFormatter(renderMessage: true),
                            @"logs\log-{Date}.log");
                    });
                });
        }
    }
}
