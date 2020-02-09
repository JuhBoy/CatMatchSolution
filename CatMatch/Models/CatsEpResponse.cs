using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Models
{
    public class CatsEpResponse
    {
        public IEnumerable<CatEp> Images { get; set; }
    }
}
