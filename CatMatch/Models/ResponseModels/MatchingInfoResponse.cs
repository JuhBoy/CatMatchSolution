using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models.ResponseModels
{
    public class MatchingInfoResponse
    {
        public int Id { get; set; }
        public int MatchCount { get; set; }
        public int Victories { get; set; }
        public IList<Match> History { get; set; }
    }
}
