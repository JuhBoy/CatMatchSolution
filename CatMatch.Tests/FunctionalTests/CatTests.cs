using CatMatch.Models;
using CatMatch.Services;
using CatMatch.Tests.Fixtures.Accessors;
using CatMatch.Tests.Fixtures.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CatMatch.Tests.FunctionalTests
{
    public sealed class CatTests
    {
        [Fact]
        public void EnsureFixtures_load()
        {
            string list = CatsModel.JsonList;
            Assert.NotNull(list);
        }

        [Fact]
        public async Task ShouldInjectCats_ProvidedByApi()
        {
            using (var dbContext = DbContextFixture.GetDbContext())
            {
                var service = new CatService(new HttpService(), dbContext, IConfigMock.GetCatsMock());
                var previousCount = dbContext.Cats.Count();

                await service.InjectCats().ConfigureAwait(false);
                Cat[] cats = dbContext.Cats.ToArray();

                Assert.NotNull(cats);
                Assert.NotEmpty(cats);
                Assert.True(previousCount == cats.Length);
            }
        }

        [Fact]
        public async Task ShouldLoadCatWithDependency_WhenFilterAskTo()
        {
            using (var dbContext = DbContextFixture.GetDbContext())
            {
                var service = new CatService(new HttpService(), dbContext, IConfigMock.GetCatsMock());
                var cat = await service.Get(1, new RequestOptions() { IncludeMatchHistory = true }).ConfigureAwait(false);

                Assert.NotNull(cat);
                Assert.NotNull(cat.Rank);
                Assert.NotNull(cat.Informations);
                Assert.Empty(cat.Informations.History);
            }
        }

        [Fact]
        public async Task ShouldLoadAllCatsWithRank_WhenOptionsFilterApplyied()
        {
            using (var dbContext = DbContextFixture.GetDbContext())
            {
                var service = new CatService(new HttpService(), dbContext, IConfigMock.GetCatsMock());
                IList<Cat> cats = (await service.GetAll(new RequestOptions()
                {
                    Count = 5,
                    Offset = 0,
                    EloFilter = new MinMaxFilter() { Min = 1200, Max = 1500 },
                    IncludeMatchHistory = true
                }).ConfigureAwait(false)).ToList();

                Assert.NotNull(cats);
                Assert.NotEmpty(cats);
                Assert.True(cats.Count == 5);
                Assert.NotNull(cats[0].Rank);
                Assert.NotNull(cats[0].Informations);
                Assert.NotNull(cats[0].Informations.History);
            }
        }
    }
}
