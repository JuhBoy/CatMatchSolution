using CatMatch.Databases.MariaDb.Models;
using CatMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CatMatch.Databases.MariaDb
{
    public class CatMatchMariaDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<MatchingInformations> MatchingInformations { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<MatchCat> MatchCats { get; set; }

        public CatMatchMariaDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            CreateCat(builder);
            CreateMatch(builder);
            CreateMatchingInfos(builder);
            CreateRank(builder);
            CreateMatchCat(builder);
        }

        private static void CreateMatchCat(ModelBuilder builder)
        {
            builder.Entity<MatchCat>(buildAction =>
            {
                buildAction.HasKey(c => c.Id).HasName("id");
                buildAction.Property(c => c.Id).HasColumnName("id");
                buildAction.HasOne<Cat>(c => c.Cat);
                buildAction.HasOne<Match>(c => c.Match);

                buildAction.Property(c => c.CatId).HasColumnName("cat_id");
                buildAction.Property(c => c.MatchId).HasColumnName("match_id");
            });
        }

        internal Cat FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        private static void CreateRank(ModelBuilder builder)
        {
            builder.Entity<Rank>(buildAction =>
            {
                buildAction.HasKey(c => c.Id).HasName("id");
                buildAction.Property(c => c.Id).HasColumnName("id");
                buildAction.Property(c => c.Elo).HasColumnName("elo");
                buildAction.Property(c => c.CatId).HasColumnName("cat_id");
            });
        }

        private static void CreateMatchingInfos(ModelBuilder builder)
        {
            builder.Entity<MatchingInformations>(buildAction =>
            {
                buildAction.HasKey(c => c.Id).HasName("id");
                buildAction.Property(c => c.Id).HasColumnName("id");
                buildAction.Property(c => c.MatchCount).HasColumnName("match_count");
                buildAction.Property(c => c.Victories).HasColumnName("victories");
                buildAction.Property(c => c.CatId).HasColumnName("CatId");
                buildAction.Ignore(c => c.History);
            });
        }

        private static void CreateMatch(ModelBuilder builder)
        {
            builder.Entity<Match>(buildAction =>
            {
                buildAction.HasKey(c => c.Id).HasName("id");
                buildAction.Property(c => c.Id).HasColumnName("id");
                buildAction.Property(c => c.DateUtc).HasColumnName("date_utc").IsRequired();
                buildAction.Property(c => c.Winner).HasColumnName("winner").IsRequired();

                buildAction.Ignore(c => c.Participant);
            });
        }

        private static void CreateCat(ModelBuilder builder)
        {
            builder.Entity<Cat>(buildAction =>
            {
                buildAction.HasKey(c => c.Id).HasName("id");
                buildAction.Property(c => c.Id).HasColumnName("id");
                buildAction.Property(c => c.ImageLink).HasColumnName("image_link");
                buildAction.Property(c => c.InformationsId).HasColumnName("information_id");
                buildAction.Property(c => c.RankId).HasColumnName("rank_id");
                buildAction.HasOne<MatchingInformations>(c => c.Informations)
                    .WithOne(c => c.Cat)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey<Cat>(c => c.InformationsId)
                    .IsRequired();
                buildAction.HasOne<Rank>(c => c.Rank)
                    .WithOne(c => c.Cat)
                    .HasForeignKey<Cat>(c => c.RankId);
            });
        }
    }
}
