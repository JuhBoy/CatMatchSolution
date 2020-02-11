namespace CatMatch.Models.ResponseModels
{
    public sealed class CatResponse
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public MatchingInfoResponse Informations { get; set; }
        public RankResponse Rank { get; set; }
    }
}