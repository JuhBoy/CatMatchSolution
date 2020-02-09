using System.Net.Http;

namespace CatMatch.Services
{
    public interface IHttpService
    {
        HttpClient Client { get; }
    }
}