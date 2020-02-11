using CatMatch.Extensions.Converter;
using CatMatch.Http.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatMatch.Tests.UnitTests
{
    public sealed class CommonResponseTests
    {

        [Fact]
        public void ShouldComputeBufferUtf8Model_WhenToJsonExtentsionMethodeCalled()
        {
            CommonResponse<string> response = new CommonResponse<string>(200, "Whoops, it failed!", "my_payload");
            
            byte[] buffer = response.ToJsonBuffer();

            Assert.NotNull(buffer);
            Assert.NotEmpty(buffer);
            Assert.Equal("{\"internalCode\":200,\"message\":\"Whoops, it failed!\",\"payload\":\"my_payload\"}", Encoding.UTF8.GetString(buffer));
        }
    }
}
