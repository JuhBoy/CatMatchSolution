namespace CatMatch.Services.Ranking
{
    public interface IRankingService
    {
        int Limit { get; }
        float GetEstimation(int eloA, int eloB);
        int GetNewElo(int oldElo, float estimation, bool victory);
    }
}