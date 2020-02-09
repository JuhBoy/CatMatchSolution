namespace CatMatch.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        
        public MatchingInformations Informations { get; set; }
        public Rank Rank { get; set; }
    }
}
