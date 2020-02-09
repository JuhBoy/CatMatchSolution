using System.Collections.Generic;

namespace CatMatch.Models
{
    public class MatchingInformations
    {
        public int Id { get; set; }
        public int MatchCount { get; set; }
        public int Victories { get; set; }

        public int CatId { get; set; }
        public Cat Cat { get; set; }

        public IList<Match> History { get; set; } = new List<Match>();
    }
}
