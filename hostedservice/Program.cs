using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Logging;

namespace hostedservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = new HostBuilder()
            //.ConfigureAppConfiguration((hostContext, config) =>
            //{
            //    var env = hostContext.HostingEnvironment;
            //    config.SetBasePath(Directory.GetCurrentDirectory());
            //    config.AddEnvironmentVariables();
            //    config.AddCommandLine(args);
            //    config.AddJsonFile("appsettings.json", true, false);
            //    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, false);
            //    config.AddCloudFoundry();
            //})
            //.ConfigureServices((hostContext, services) =>
            //{
            //    ConfigureServices(services, hostContext.Configuration);
            //})
            //.ConfigureLogging((hostContext, logBuilder) =>
            //{
            //    logBuilder.ClearProviders();
            //    logBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
            //    logBuilder.AddDynamicConsole();
            //    if (hostContext.HostingEnvironment.IsDevelopment())
            //    {
            //        logBuilder.AddDebug();
            //    }
            //});
            //await host.RunConsoleAsync();

            BuildWebHost(args).Run();
            Console.WriteLine("Finished executing task!");
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost
           .CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .ConfigureAppConfiguration((builderContext, config) =>
           {
               config.AddCloudFoundry();
           })
           .ConfigureLogging((hostContext, logBuilder) =>
           {
               logBuilder.ClearProviders();
               logBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
               logBuilder.AddDynamicConsole();
               if (hostContext.HostingEnvironment.IsDevelopment())
               {
                   logBuilder.AddDebug();
               }
           }).Build();
    }
}
