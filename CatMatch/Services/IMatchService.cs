using CatMatch.Models;
using System.Threading.Tasks;

namespace CatMatch.Services
{
    public interface IMatchService
    {
        Task MatchAsync(int leftCatId, int rightCatId, int winnerId);
        Task<MatchIds> FindMatchAsync();
    }
}