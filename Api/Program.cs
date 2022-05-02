using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UnaPinta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (enviroment.Equals("Local"))
                        webBuilder.UseStartup<Startup>();
                    else
                    {
                        var port = Environment.GetEnvironmentVariable("PORT");

                        webBuilder.UseStartup<Startup>()
                            .UseUrls("http://*:" + port);
                    }
                    
                });
    }
}
