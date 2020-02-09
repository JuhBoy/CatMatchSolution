using CatMatch.Databases.MariaDb;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace CatMatch.Tests.Fixtures.Accessors
{

    public static class DbContextFixture
    {
        public static CatMatchMariaDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CatMatchMariaDbContext>();
            options.UseMySql("Server=localhost;Database=catmatch;User=root;Password=root;", (actions) =>
            {
                actions.ServerVersion(new Version(10, 3, 14), ServerType.MariaDb);
            });
            return new CatMatchMariaDbContext(options.Options);
        }
    }
}
