using CatMatch.Extensions.Models;
using CatMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Services.Filters
{
    public sealed class CatRequestFilter
    {

        public CatRequestFilter(RequestOptions options)
        {
            Options = options;
        }

        RequestOptions Options { get; }

        public void Process(ref IQueryable<Cat> query)
        {
            query = FilterCatElo(query);
            query = FilterCatWinRate(query);
            query = query.OrderBy(c => c.Rank.Elo);
            query = FilterMinMax(query);
        }

        private IQueryable<Cat> FilterMinMax(IQueryable<Cat> query)
        {
            if (Options.Offset.HasValue)
                query = query.Skip(Options.Offset.Value);
            if (Options.Count.HasValue)
                query = query.Take(Options.Count.Value);
            return query;
        }

        private IQueryable<Cat> FilterCatWinRate(IQueryable<Cat> query)
        {
            if (Options.WinRateFilter.HasValue)
                query = query.Where(c => c.GetWinRate() >= Options.WinRateFilter.Value.Min &&
                                         c.GetWinRate() <= Options.WinRateFilter.Value.Max);
            return query;
        }

        private IQueryable<Cat> FilterCatElo(IQueryable<Cat> query)
        {
            if (Options.EloFilter.HasValue)
                query = query.Where(c => c.Rank.Elo >= Options.EloFilter.Value.Min &&
                                         c.Rank.Elo <= Options.EloFilter.Value.Max);
            return query;
        }
    }
}
