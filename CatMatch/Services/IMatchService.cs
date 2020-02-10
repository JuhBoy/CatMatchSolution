using System.Threading.Tasks;

namespace CatMatch.Services
{
    public interface IMatchService
    {
        Task Match(int leftCatId, int rightCatId, int winnerId);
    }
}