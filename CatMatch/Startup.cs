using CatMatch.Extensions.Configuration;
using CatMatch.Middlewares;
using CatMatch.Services;
using CatMatch.Services.Ranking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CatMatch
{
    public class Startup
    {
        public static JsonSerializerSettings DefaultJsonSettings => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() },
            Formatting = Formatting.None
        };

    public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMariaDb(Configuration["MariaDb:ConnectionString"]);

            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ICatService, CatService>();
            services.AddScoped<IRankingService, RankingService>(options =>
            {
                int limit = Configuration.GetValue<int>("Ranking:Limit");
                int evolutionCoef = Configuration.GetValue<int>("Ranking:EvolutionCoef");
                return new RankingService(limit, evolutionCoef);
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist"; // In production, the Angular files will be served from this directory
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMatchExceptionHandler();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
