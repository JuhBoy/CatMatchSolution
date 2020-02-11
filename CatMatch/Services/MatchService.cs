using CatMatch.Extensions.Database;
using CatMatch.Http.Models;
using CatMatch.Models;
using CatMatch.Services.Ranking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task MatchAsync(int leftCatId, int rightCatId, int winnerId)
        {
            Cat left = await Context.Cats.IncludeSubModels().FirstOrDefaultAsync(c => c.Id == leftCatId).ConfigureAwait(false);
            Cat right = await Context.Cats.IncludeSubModels().FirstOrDefaultAsync(c => c.Id == rightCatId).ConfigureAwait(false);

            if (left == null || right == null)
                throw new ServiceException("Cat Id Invalid", ResponseCode.DatabaseNotFound);

            float leftEstimation = RankingService.GetEstimation(left.Rank.Elo, right.Rank.Elo);
            float rightEstimation = RankingService.GetEstimation(right.Rank.Elo, left.Rank.Elo);

            left.Rank.Elo = RankingService.GetNewElo(left.Rank.Elo, leftEstimation, leftCatId == winnerId);
            right.Rank.Elo = RankingService.GetNewElo(right.Rank.Elo, rightEstimation, rightCatId == winnerId);

            Context.Cats.UpdateRange(left, right);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<MatchIds> FindMatchAsync()
        {
            int max = await Context.Ranks.MaxAsync(a => a.Elo).ConfigureAwait(false);
            int min = await Context.Ranks.MinAsync(a => a.Elo).ConfigureAwait(false);

            Random seed = new Random();
            IList<Rank> superSet = null;
            int currentMax = max;

            while (currentMax != 0 && (superSet == null || superSet.Count < 2))
            {
                currentMax = (superSet == null) ? max : max / 2;
                FindBoundaries(min, currentMax, seed, out int lowerBound, out int higherBound);
                superSet = Context.Ranks.OrderBy(e => e.Elo).Where(e => e.Elo < higherBound && e.Elo > lowerBound).ToList();
            }

            if (superSet == null || superSet.Count < 2)
                throw new ServiceException("Couldn't find a match", ResponseCode.MatchDispersion);

            var ids = new MatchIds() { Left = 0, Right = 0 };
            ids.Left = seed.Next(0, superSet.Count);
            ids.Right = (ids.Left == 0) ? 1 : ids.Left - 1;

            return ids;
        }

        private void FindBoundaries(int min, int max, Random seed, out int lowerBound, out int higherBound)
        {
            int point = seed.Next(min, max);
            lowerBound = point - RankingService.Limit;
            higherBound = point + RankingService.Limit;
        }
    }
}
