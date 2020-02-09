using System.Net.Http;

namespace CatMatch.Services
{
    public class HttpService : IHttpService
    {
        public HttpClient Client { get; }

        public HttpService()
        {
            Client = new HttpClient();
        }

        public HttpService(HttpMessageHandler handler, bool disposeHandler)
        {
            Client = new HttpClient(handler, disposeHandler);
        }
    }
}
