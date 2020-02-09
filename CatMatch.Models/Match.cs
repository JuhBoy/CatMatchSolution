using System;
using System.Collections.Generic;

namespace CatMatch.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime DateUtc { get; set; } = DateTime.UtcNow;
        public int Winner { get; set; }

        public IList<Cat> Participant { get; set; }
    }
}
