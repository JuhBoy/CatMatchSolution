using CatMatch.Models;
using CatMatch.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Extensions.Database
{
    public static class InformationExtensions
    {
        public static MatchingInfoResponse ToApiModel(this MatchingInformations info)
        {
            return new MatchingInfoResponse()
            {
                Id = info.Id,
                History = info.History,
                MatchCount = info.MatchCount,
                Victories = info.Victories
            };
        }
    }
}
