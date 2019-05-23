using System.Threading.Tasks;
using Core.Domain.ViewModels;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1
{
    [Route("/api/v1/commercialCourse")]
    public class CourseRegistrationApiController : RootApiController
    {
        private readonly ICandidateService _candidateService;

        public CourseRegistrationApiController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CourseRegistration courseRegistration)
        {
            var result = await _candidateService.Register(courseRegistration);
            return result.CandidateRegistrationResponse != CandidateRegistrationResponse.Successful ? Json(new {Success = false, Response = result.CandidateRegistrationResponse})
                : Json(new {Success = true});
        }
    }
}