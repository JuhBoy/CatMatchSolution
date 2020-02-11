using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models.ResponseModels
{
    public class MatchResponse
    {
        public int Id { get; set; }
        public DateTime DateUtc { get; set; }
        public int Winner { get; set; }
    }
}
