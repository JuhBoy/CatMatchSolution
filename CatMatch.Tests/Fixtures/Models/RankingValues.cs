using System;
using System.Collections.Generic;
using System.Text;

namespace CatMatch.Tests.Fixtures.Models
{
    internal static class RankingValues
    {
        public static readonly int Limit = 400;
        public static readonly int EvolutionCoef = 24;

        public static readonly int LeftValidValue = 2_300;
        public static readonly int RightValidValue = 1_800;

        public static readonly float PreCalculatedLeftEstimation = 0.947F;

        public static readonly int LeftWonExpectedNewElo = 2_301;
        public static readonly int LeftLostExpectedNewElo = 2_277;
    }
}
