using CatMatch.Extensions.Database;
using CatMatch.Http.Models;
using CatMatch.Models;
using CatMatch.Models.ResponseModels;
using CatMatch.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMatch.Controllers
{
    [Route("api/match"), ApiController]
    public class MatchController : Controller
    {

        public MatchController(ICatService catService, IMatchService matchService)
        {
            CatService = catService;
            MatchService = matchService;
        }

        private ICatService CatService { get; set; }
        private IMatchService MatchService { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetMatch()
        {
            MatchIds matchIds = await MatchService.FindMatchAsync().ConfigureAwait(false);
            var options = new RequestOptions() { IncludeMatchHistory = false };

            Cat left = await CatService.Get(matchIds.Left, options).ConfigureAwait(false);
            Cat right = await CatService.Get(matchIds.Right, options).ConfigureAwait(false);
            MatchFoundResponse matchFound = new MatchFoundResponse() { Left = left.ToApiModel(), Right = right.ToApiModel() };

            var response = new CommonResponse<MatchFoundResponse>(ResponseCode.Success, "Ok", matchFound);

            return Ok(response);
        }

        [HttpGet("{leftId}/{rightId}/{winnerId}")]
        public async Task<IActionResult> ResolveMatch(int leftId, int rightId, int winnerId)
        {
            await MatchService.MatchAsync(leftId, rightId, winnerId).ConfigureAwait(false);
            return Ok(new CommonResponse<string>(ResponseCode.Success, "Match Resolved", ""));
        }
    }
}
