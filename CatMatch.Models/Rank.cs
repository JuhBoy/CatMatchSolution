namespace CatMatch.Models
{
    public class Rank
    {
        public int Id { get; set; }
        public int Elo { get; set; }

        public int CatId { get; set; }
        public Cat Cat { get; set; }
    }
}
