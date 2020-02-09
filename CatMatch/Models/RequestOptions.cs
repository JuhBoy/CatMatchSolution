using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models
{
    public class RequestOptions
    {
        public bool IncludeMatchHistory { get; set; }
        public int? Offset { get; set; }
        public int? Count { get; set; }

        public MinMaxFilter? EloFilter { get; set; }
        public MinMaxFilter? WinRateFilter { get; set; }
    }
}
