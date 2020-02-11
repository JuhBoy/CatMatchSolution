using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models.ResponseModels
{
    public class RankResponse
    {
        public int Id { get; set; }
        public int Elo { get; set; }
    }
}
