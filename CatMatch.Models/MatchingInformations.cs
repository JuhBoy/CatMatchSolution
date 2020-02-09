using System.Collections.Generic;

namespace CatMatch.Models
{
    public class MatchingInformations
    {
        public int CatId { get; set; }
        public int MatchCount { get; set; }
        public int Victories { get; set; }

        public IList<Match> History { get; set; }
    }
}
