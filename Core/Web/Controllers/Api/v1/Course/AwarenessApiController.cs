using System.Threading.Tasks;
using Core.Domain.Models.Course;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1.Course
{
    [Authorize(Roles= "Standard")]
    [Route("/api/v1/course/awareness")]
    public class AwarenessCourseApiController : RootApiController
    {
        private readonly ICandidateService _candidateService;

        public AwarenessCourseApiController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // [HttpPost("register")]
        // public async Task<IActionResult> Register([FromBody] AwarenessRegistrationRequest awarenessRegistration)
        // {
        //     var result = await _candidateService.Register(awarenessRegistration);
        //     return result.CandidateRegistrationResponse != CandidateRegistrationResponse.Successful
        //         ? Json(new {Success = false, Response = result.CandidateRegistrationResponse})
        //         : Json(new {Success = true});
        // }
    }
}