using System;
using System.Text.RegularExpressions;

namespace CatMatch.Models
{
    public struct MinMaxFilter
    { 
        public int Max { get; set; }
        public int Min { get; set; }

        internal static MinMaxFilter? TryParse(string serializedFilter)
        {
            if (serializedFilter == null) return null;

            Regex regex = new Regex("([minax]{3}:\\d+)");
            var match = regex.Matches(serializedFilter);

            if (match.Count != 2)
                return null;

            bool minParsed = int.TryParse(match[0].Value.Split(":")[1], out var min);
            bool maxParsed = int.TryParse(match[1].Value.Split(":")[1], out var max);

            if (!minParsed || !maxParsed)
                return null;

            return new MinMaxFilter() { Max = max, Min = min };
        }
    }
}