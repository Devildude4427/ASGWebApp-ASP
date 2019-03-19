using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.v1
{
    [Route("/api/v1/statistics")]
    public class StatisticsApiController : RootApiController
    {
        private readonly CandidateService _candidateService;

        public StatisticsApiController(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        
        [HttpGet("candidateCount")]
        public async Task<IActionResult> GetCurrentCandidateCount()
        {
            var result = await _candidateService.GetCurrentCandidateCount();
            Console.WriteLine(result);
            return Json(result);
        }
    }
}