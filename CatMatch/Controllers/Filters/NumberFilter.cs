using CatMatch.Http.Models;
using CatMatch.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace CatMatch.Controllers.Filters
{
    public class NumberFilter : ActionFilterAttribute
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = int.MaxValue;
        public string QueryKey { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!string.IsNullOrEmpty(QueryKey))
            {
                string query = context.HttpContext.Request.Query[QueryKey];
                if (!string.IsNullOrEmpty(query))
                {
                    int number = int.Parse(query);
                    if (number <= Min || number >= Max)
                        throw new ServiceException($"GET parameter <{QueryKey}> is Invalid, Must be between [{Min};{Max}]", ResponseCode.InvalidRequest);
                }
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
