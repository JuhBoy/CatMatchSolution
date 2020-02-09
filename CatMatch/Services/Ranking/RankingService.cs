using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Services.Ranking
{
    public class RankingService : IRankingService
    {
        public static readonly int DefaultElo = 1_500;

        public RankingService() { }

        public RankingService(int limit, int evolutionCoef)
        {
            if (limit <= 0 || evolutionCoef <= 0)
                throw new Exception($"Invalid Data provided, Limit({limit}) - Coef({evolutionCoef})");

            Limit = limit;
            EvolutionCoef = evolutionCoef;
        }

        private int Limit { get; }
        private int EvolutionCoef { get; }

        public float GetEstimation(int eloA, int eloB)
        {
            if (!ValidateElo(eloA) || !ValidateElo(eloB))
                throw new Exception($"Invalid Elo provided {eloA} {eloB}");

            float delta = (float)(eloB - eloA) / Limit;
            double estimation = 1 / (1 + Math.Pow(10, delta));
            return (float)Math.Round(estimation, 3);
        }

        public int GetNewElo(int oldElo, float estimation, bool victory)
        {
            int score = (victory) ? 1 : 0;
            float newElo = oldElo + (EvolutionCoef * (score - estimation));

            if (newElo < Limit)
                newElo = Limit;

            return (int)Math.Round(newElo, 0);
        }

        private bool ValidateElo(int elo)
        {
            return (elo >= Limit);
        }
    }
}
