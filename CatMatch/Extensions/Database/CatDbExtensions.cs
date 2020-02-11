using CatMatch.Models;
using CatMatch.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMatch.Extensions.Database;

namespace CatMatch.Extensions.Database
{
    public static class CatDbExtensions
    {
        public static CatsResponse ToApiModel(this IEnumerable<Cat> cats)
        {
            CatResponse[] catsRsp = cats.Select(c => c.ToApiModel()).ToArray();

            return new CatsResponse()
            {
                Cats = catsRsp,
                Count = catsRsp.Length
            };
        }

        public static CatResponse ToApiModel(this Cat cat)
        {
            return new CatResponse()
            {
                Id = cat.Id,
                ImageLink = cat.ImageLink,
                Informations = cat.Informations.ToApiModel(),
                Rank = cat.Rank.ToApiModel()
            };
        }
    }
}
