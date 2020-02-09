using CatMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Databases.MariaDb.Models
{
    public class MatchCat
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public int MatchId { get; set; }

        public Cat Cat { get; set; }
        public Match Match { get; set; }
    }
}
