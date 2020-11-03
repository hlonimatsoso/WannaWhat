using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WannaWhat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Read Configuration from appSettingsss
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();



            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            try
            {
                Log.Warning("Wanna What Starting....");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Wanna What Running.");

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Osiris Explorer failed to start. Error Message : '{ex.Message}'");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
