using CatMatch.Models;
using CatMatch.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Extensions.Database
{
    public static class RankExtensions
    {
        public static RankResponse ToApiModel(this Rank rank)
        {
            return new RankResponse() { Elo = rank.Elo, Id = rank.Id };
        }
    }
}
