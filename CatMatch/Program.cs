using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMatch.Databases.MariaDb;
using CatMatch.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatMatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<CatMatchMariaDbContext>();
                    dbContext.Database.EnsureCreated();

                    var catsService = services.GetRequiredService<ICatService>();
                    catsService.InjectCats();

                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
