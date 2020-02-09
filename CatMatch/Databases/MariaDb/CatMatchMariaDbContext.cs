using Microsoft.EntityFrameworkCore;

namespace CatMatch.Databases.MariaDb
{
    public class CatMatchMariaDbContext : DbContext
    {

        public CatMatchMariaDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Silence is golden
        }
    }
}
