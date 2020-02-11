using CatMatch.Databases.MariaDb;
using CatMatch.Extensions.Database;
using CatMatch.Models;
using CatMatch.Services;
using CatMatch.Services.Ranking;
using CatMatch.Tests.Fixtures.Accessors;
using CatMatch.Tests.Fixtures.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CatMatch.Tests.FunctionalTests
{
    public sealed class MatchTests
    {

        [Fact]
        public async Task ShouldUpdateEloRank_WhenLeftSideWin()
        {
            using (var dbContext = DbContextFixture.GetDbContext())
            {
                IMatchService service = new MatchService(dbContext, new RankingService(RankingValues.Limit, RankingValues.EvolutionCoef));
                int cat1 = 1, winner = 1, cat2 = 2;

                var elos = await LoadElo(cat1, cat2, dbContext).ConfigureAwait(false);

                await service.MatchAsync(cat1, cat2, winner).ConfigureAwait(false);

                var newElos = await LoadElo(cat1, cat2, dbContext).ConfigureAwait(false);

                Assert.NotEqual(elos.EloLeft, newElos.EloLeft);
                Assert.NotEqual(elos.EloRight, newElos.EloRight);
            }
        }

        [Fact]
        public async Task ShouldFindMatch_WhenCatsAreAvailable()
        {
            using (var dbContext = DbContextFixture.GetDbContext())
            {
                IMatchService service = new MatchService(dbContext, new RankingService(RankingValues.Limit, RankingValues.EvolutionCoef));
                ICatService catService = new CatService(new HttpService(), dbContext, IConfigMock.GetCatsMock());
                await catService.InjectCats().ConfigureAwait(false); // Prepare cats

                MatchIds matchIds = await service.FindMatchAsync().ConfigureAwait(false);

                Assert.NotEqual(0, matchIds.Left);
                Assert.NotEqual(0, matchIds.Right);
            }
        }

        private async Task<(int EloLeft, int EloRight)> LoadElo(int leftId, int rightId, CatMatchMariaDbContext context)
        {
            var left = await context.Cats.IncludeSubModels().AsNoTracking()
                .Select(e => new { Id = e.Id, Elo = e.Rank.Elo })
                .FirstOrDefaultAsync(c => c.Id == leftId)
                .ConfigureAwait(false);

            var right = await context.Cats.IncludeSubModels().AsNoTracking()
                .Select(e => new { Id = e.Id, Elo = e.Rank.Elo })
                .FirstOrDefaultAsync(c => c.Id == rightId)
                .ConfigureAwait(false);

            return (left.Elo, right.Elo);
        }
    }
}
