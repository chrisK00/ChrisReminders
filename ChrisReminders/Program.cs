using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChrisReminders
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            var host = new HostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", false, true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(Log.Logger);
                    services.AddSingleton<MailService>();
                    services.Configure<MailSettings>(hostContext.Configuration.GetSection(nameof(MailSettings)));
                    services.AddScheduler();
                    services.AddTransient<CheckReminders>();
                    services.AddSingleton<ReminderRepository>();

                }).Build();

            host.Services.UseScheduler(scheduler =>
                scheduler
                    .Schedule<CheckReminders>()
                    .EveryFiveSeconds()
            );

            host.Run();




        }

    }
}
