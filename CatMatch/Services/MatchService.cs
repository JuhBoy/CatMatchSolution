using CatMatch.Extensions.Database;
using CatMatch.Models;
using CatMatch.Services.Ranking;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatMatchContext = CatMatch.Databases.MariaDb.CatMatchMariaDbContext;

namespace CatMatch.Services
{
    public class MatchService : IMatchService
    {
        public MatchService(CatMatchContext context, IRankingService rankingService)
        {
            Context = context;
            RankingService = rankingService;
        }

        private CatMatchContext Context { get; set; }
        private IRankingService RankingService { get; set; }

        public async Task Match(int leftCatId, int rightCatId, int winnerId)
        {
            Cat left = await Context.Cats.IncludeSubModels().FirstOrDefaultAsync(c => c.Id == leftCatId).ConfigureAwait(false);
            Cat right = await Context.Cats.IncludeSubModels().FirstOrDefaultAsync(c => c.Id == rightCatId).ConfigureAwait(false);

            if (left == null || right == null)
                throw new ServiceException("Cat Id Invalid");

            float leftEstimation = RankingService.GetEstimation(left.Rank.Elo, right.Rank.Elo);
            float rightEstimation = RankingService.GetEstimation(right.Rank.Elo, left.Rank.Elo);

            left.Rank.Elo = RankingService.GetNewElo(left.Rank.Elo, leftEstimation, leftCatId == winnerId);
            right.Rank.Elo = RankingService.GetNewElo(right.Rank.Elo, rightEstimation, rightCatId == winnerId);

            Context.Cats.UpdateRange(left, right);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
