using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RecipeApiApp.Api {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder (args).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host
            .CreateDefaultBuilder (args)
            .ConfigureAppConfiguration ((hostingContext, config) => {
                config.AddJsonFile ("appSettings.json", optional : true, reloadOnChange : true);
                config.AddJsonFile ("appSettings.Development.json", optional : true, reloadOnChange : false);
                config.AddEnvironmentVariables ();
            })
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}