using System.Security.Claims;
using System.Threading.Tasks;
using Core.Domain.Models.Course;
using Core.Services;
using Core.Services.Course;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1.Course
{
    [Route("/api/v1/course/commercial")]
    public class CommercialCourseApiController : RootApiController
    {
        private readonly ICommercialService _commercialService;

        public CommercialCourseApiController(ICommercialService commercialService)
        {
            _commercialService = commercialService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CommercialRegistrationRequest commercialRegistration)
        {
            var result = await _commercialService.Register(commercialRegistration, User.FindFirstValue(ClaimTypes.Email));
            return result.CandidateRegistrationResponse != CandidateRegistrationResponse.Successful
                ? Json(new {Success = false, Response = result.CandidateRegistrationResponse})
                : Json(new {Success = true});
        }
    }
}