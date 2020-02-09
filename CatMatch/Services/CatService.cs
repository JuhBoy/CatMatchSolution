using CatMatchContext = CatMatch.Databases.MariaDb.CatMatchMariaDbContext;

namespace CatMatch.Services
{
    public class CatService : ICatService
    {

        public CatService(IHttpService httpService, CatMatchContext context)
        {
            HttpService = httpService;
            Context = context;
        }

        private IHttpService HttpService { get; }
        private CatMatchContext Context { get; }
    }
}
