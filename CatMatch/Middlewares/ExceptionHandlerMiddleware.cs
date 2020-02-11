using CatMatch.Extensions.Converter;
using CatMatch.Http.Models;
using CatMatch.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CatMatch.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate Next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext).ConfigureAwait(false);
            }
            catch (ServiceException ex)
            {
                httpContext.Response.StatusCode = 200;
                await CreateResponseOnException(httpContext, ex.InternalCode, ex);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 200;
                await CreateResponseOnException(httpContext, ResponseCode.UnknownError, ex).ConfigureAwait(false);
            }
        }

        private static async Task CreateResponseOnException(HttpContext httpContext, int status, Exception ex)
        {
            var response = new CommonResponse<Exception>(status, ex.Message, ex);
            byte[] buffer = response.ToJsonBuffer();
            await httpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
        }
    }
}
