using CatMatch.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Extensions.Configuration
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseMatchExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
