using CatMatch.Services.Ranking;
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
    }
}
