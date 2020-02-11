using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models.ResponseModels
{
    public sealed class CatsResponse
    {
        public int Count { get; set; }
        public IEnumerable<CatResponse> Cats { get; set; }
    }
}
