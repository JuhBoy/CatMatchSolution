using CatMatch.Services;
using CatMatch.Tests.Fixtures.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CatMatch.Tests.FunctionalTests
{
    public sealed class HttpServiceTests
    {

        [Fact]
        public async Task ShouldLoadCats_FromValidUrl()
        {
            IHttpService service = new HttpService();
            Uri uri = new Uri(HttpValues.CatsUrl);

            HttpResponseMessage response = await service.Client.GetAsync(uri).ConfigureAwait(false);

            string body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(body);
        }
    }
}
