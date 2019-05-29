using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1
{
    [Authorize(Roles = "Admin")]
    [Route("/api/v1/statistics")]
    public class StatisticsApiController : RootApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
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