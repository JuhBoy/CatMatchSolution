using CatMatch.Controllers.Filters;
using CatMatch.Extensions.Database;
using CatMatch.Http.Models;
using CatMatch.Models;
using CatMatch.Models.ResponseModels;
using CatMatch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatMatch.Controllers
{
    [Route("api/cats"), ApiController]
    public class CatController : Controller
    {

        public CatController(ICatService catService)
        {
            CatService = catService;
        }

        ICatService CatService { get; }

        /// <summary>
        /// Return all cats models.
        /// </summary>
        /// <param name="offset">Skiped indexes</param>
        /// <param name="amount">Number of elements</param>
        /// <param name="includeHistory">Include history model for the cats</param>
        /// <param name="eloFilter">Filter by range of Elo (Format: &elo-filter=min:[0-9];max:[0-9]</param>
        /// <param name="winrateFilter">Filter by range of Win Rate (Format: &elo-filter=min:[0-9];max:[0-9]</param>
        /// <returns></returns>
        [HttpGet, NumberFilter(QueryKey = "offset", Min = 0), NumberFilter(QueryKey = "amount", Min = 0)]
        public async Task<IActionResult> GetAll([FromQuery(Name = "offset")] int? offset,
                                                [FromQuery(Name = "amount")] int? amount,
                                                [FromQuery(Name = "history")] bool? includeHistory,
                                                [FromQuery(Name = "elo-filter")] string eloFilter,
                                                [FromQuery(Name = "winrate-filter")] string winrateFilter)
        {
            bool loadHistory = (includeHistory.HasValue) ? includeHistory.Value : false;
            var requestOptions = new RequestOptions()
            {
                Count = amount,
                Offset = offset,
                IncludeMatchHistory = loadHistory,
                EloFilter = MinMaxFilter.TryParse(eloFilter),
                WinRateFilter = MinMaxFilter.TryParse(winrateFilter)
            };

            IEnumerable<Cat> cats = await CatService.GetAll(requestOptions).ConfigureAwait(false);
            var response = new CommonResponse<CatsResponse>(ResponseCode.Success, "Ok", cats.ToApiModel());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery(Name = "history")] bool? includeHistory)
        {
            bool loadHistory = (includeHistory.HasValue) ? includeHistory.Value : false;

            Cat cat = await CatService.Get(id, new RequestOptions() { IncludeMatchHistory = loadHistory }).ConfigureAwait(false);
            var response = new CommonResponse<CatResponse>(ResponseCode.Success, "Ok", cat.ToApiModel());

            return Ok(response);
        }
    }
}
