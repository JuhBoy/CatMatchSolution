using CatMatch.Databases.MariaDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace CatMatch.Extensions.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMariaDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CatMatchMariaDbContext>(options =>
            {
                options.UseMySql(connectionString, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new ServerVersion(new Version(10, 4, 0), ServerType.MariaDb));
                });
            });
        }
    }
}
