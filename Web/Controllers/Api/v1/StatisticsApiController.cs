using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers.Api.v1
{
    [Authorize]
    [Route("/api/v1/statistics")]
    public class StatisticsApiController : RootApiController
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsApiController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        
        [HttpGet("candidateCount")]
        public async Task<IActionResult> GetCurrentCandidateCount()
        {
            var result = await _statisticsService.GetCurrentCandidateCount();
            return Json(result);
        }
        
        [HttpGet("newCandidateCount")]
        public async Task<IActionResult> GetNewCandidateCount()
        {
            var result = await _statisticsService.GetNewCandidateCount();
            return Json(result);
        }
    }
}