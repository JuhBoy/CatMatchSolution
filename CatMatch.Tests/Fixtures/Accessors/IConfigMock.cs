using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatMatch.Tests.Fixtures.Accessors
{
    public static class IConfigMock
    {
        public static IConfiguration GetCatsMock()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(a => a["Cats:EndPoint"]).Returns("https://latelier.co/data/cats.json");

            return mock.Object;
        }
    }
}
