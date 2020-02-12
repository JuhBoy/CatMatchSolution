using CatMatch.Models;
using CatMatch.Services.Ranking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Extensions.Models
{
    public static class CatExtensions
    {

        public static IEnumerable<Cat> ToCatModels(this CatsEpResponse response)
        {
            return response.Images.ToCatModels();
        }

        public static IEnumerable<Cat> ToCatModels(this IEnumerable<CatEp> cats)
        {
            return cats.Select(cat => cat.ToCatModel());
        }

        public static Cat ToCatModel(this CatEp cat)
        {
            return new Cat()
            {
                ImageLink = cat.Url,
                Informations = new MatchingInformations()
                {
                    MatchCount = 0,
                    Victories = 0
                },
                Rank = new Rank()
                {
                    Elo = RankingService.DefaultElo
                }
            };
        }

        public static int GetWinRate(this Cat cat)
        {
            return cat.Informations.Victories / cat.Informations.MatchCount * 100;
        }
    }
}
