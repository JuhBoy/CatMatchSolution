using System;
using Xunit;
using CatMatch.Services.Ranking;
using CatMatch.Tests.Fixtures.Models;

namespace CatMatch.Tests.UnitTests
{
    public sealed class RankingTests
    {

        private IRankingService CreateService => new RankingService(RankingValues.Limit, RankingValues.EvolutionCoef);

        [Fact]
        public void ShouldCalculateEstimation_WhenGoodValuesAreProvided()
        {
            IRankingService rankingService = CreateService;

            float estimation = rankingService.GetEstimation(RankingValues.LeftValidValue, RankingValues.RightValidValue);

            Assert.Equal(RankingValues.PreCalculatedLeftEstimation, estimation);
        }

        [Fact]
        public void ShouldThrow_WhenInvalidElosAreProvided()
        {
            IRankingService rankingService = CreateService;

            Assert.Throws<Exception>(() =>
            {
                float estimation = rankingService.GetEstimation(-12, 0);
            });
        }

        [Fact]
        public void ShouldComputeNewElo_WhenVictory()
        {
            IRankingService rankingService = CreateService;
            float estimation = rankingService.GetEstimation(RankingValues.LeftValidValue, RankingValues.RightValidValue);
            
            int newElo = rankingService.GetNewElo(RankingValues.LeftValidValue, estimation, true);

            Assert.Equal(RankingValues.LeftWonExpectedNewElo, newElo);
        }

        [Fact]
        public void ShouldComputeNewElo_WhenLost()
        {
            IRankingService rankingService = CreateService;
            float estimation = rankingService.GetEstimation(RankingValues.LeftValidValue, RankingValues.RightValidValue);

            int newElo = rankingService.GetNewElo(RankingValues.LeftValidValue, estimation, false);

            Assert.Equal(RankingValues.LeftLostExpectedNewElo, newElo);
        }

        [Fact]
        public void ShouldLimitElo_WhenToMuchPointsAreLost()
        {
            IRankingService rankingService = CreateService;
            float estimation = rankingService.GetEstimation(RankingValues.Limit, RankingValues.Limit);

            int newElo = rankingService.GetNewElo(RankingValues.Limit, estimation, false);

            Assert.Equal(RankingValues.Limit, newElo);
        }
    }
}
