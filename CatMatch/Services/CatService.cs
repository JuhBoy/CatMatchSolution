using CatMatch.Extensions.Models;
using CatMatch.Http.Models;
using CatMatch.Models;
using CatMatch.Services.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMatchContext = CatMatch.Databases.MariaDb.CatMatchMariaDbContext;

namespace CatMatch.Services
{
    public class CatService : ICatService
    {

        public CatService(IHttpService httpService, CatMatchContext context, IConfiguration config)
        {
            HttpService = httpService;
            Context = context;
            CatsApiEndPoint = new Uri(config["Cats:EndPoint"]);
        }

        private IHttpService HttpService { get; }
        private CatMatchContext Context { get; }
        private Uri CatsApiEndPoint { get; }

        public async Task<Cat> Get(int id, RequestOptions options)
        {
            Cat cat = await Context.Cats.AsNoTracking()
                .Include(a => a.Rank)
                .Include(a => a.Informations)
                .FirstOrDefaultAsync(c => c.Id.Equals(id))
                .ConfigureAwait(false);

            if (cat == null)
                throw new ServiceException($"Entry not found for id {id}", ResponseCode.DatabaseNotFound);

            if (options != null && options.IncludeMatchHistory)
            {
                cat.Informations.History = Context.MatchCats.AsNoTracking()
                    .Where(e => e.CatId == id)
                    .Select(e => e.Match)
                    .ToArray();
            }

            return cat;
        }

        public async Task<IEnumerable<Cat>> GetAll(RequestOptions options)
        {
            IQueryable<Cat> query = Context.Cats.Include(c => c.Rank).Include(c => c.Informations).AsNoTracking();

            var catFilter = new CatRequestFilter(options);
            catFilter.Process(ref query);

            if (options.IncludeMatchHistory)
                await IncludeMatchHistory(query).ConfigureAwait(false);

            return query;
        }

        private async Task IncludeMatchHistory(IQueryable<Cat> query)
        {
            IEnumerable<int> catIds = query.Select(a => a.Id);

            // Maria Db doesn't support Limit & IN in subquery ...
            // But the ORM keep trying to use it anyway on Contains() translations
            string sql = $"SELECT * FROM matchcats WHERE cat_id IN ({string.Join(',', catIds)})";
            IDictionary<int, Match> catMatchs = await Context.MatchCats.FromSqlRaw(sql)
                .Include(c => c.Match)
                .AsNoTracking()
                .ToDictionaryAsync(a => a.CatId, a => a.Match)
                .ConfigureAwait(false);

            foreach (var cat in query)
            {
                if (catMatchs.ContainsKey(cat.Id))
                    cat.Informations.History.Add(catMatchs[cat.Id]);
            }
        }

        #region Cats Injection
        public async Task InjectCats()
        {
            string body = await LoadCatsFromApi().ConfigureAwait(false);
            CatsEpResponse catsFromEndPoint = JsonConvert.DeserializeObject<CatsEpResponse>(body);
            IEnumerable<CatEp> newCats = FilterCatsInBase(catsFromEndPoint);

            await Context.AddRangeAsync(newCats.ToCatModels()).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        private IEnumerable<CatEp> FilterCatsInBase(CatsEpResponse catsFromEndPoint)
        {
            IEnumerable<string> catUrls = catsFromEndPoint.Images.Select(e => e.Url);

            if (Context.Cats.Count() > 0)
            {
                IDictionary<string, int> exclusionMap = Context.Cats.Where(cat => catUrls.Contains(cat.ImageLink)).ToDictionary(c => c.ImageLink, c => c.Id);
                IEnumerable<CatEp> newCats = catsFromEndPoint.Images.Where(cat => !exclusionMap.ContainsKey(cat.Url));
                return newCats;
            } else
            {
                return catsFromEndPoint.Images;
            }
        }

        private async Task<string> LoadCatsFromApi()
        {
            var response = await HttpService.Client.GetAsync(CatsApiEndPoint).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new ServiceException($"Can't fetch cats Reason: {response.ReasonPhrase}", ResponseCode.UnavailableService);

            string body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return body;
        }
        #endregion
    }
}
