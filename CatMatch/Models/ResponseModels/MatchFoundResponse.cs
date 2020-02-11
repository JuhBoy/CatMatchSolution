using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models.ResponseModels
{
    public class MatchFoundResponse
    {
        public CatResponse Left { get; set; }
        public CatResponse Right { get; set; }
    }
}
