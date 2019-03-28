using System.Threading.Tasks;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.v1
{
    [Route("/api/v1/candidate")]
    public class CourseRegistrationApiController : RootApiController
    {
        private readonly CandidateService _candidateService;

        public CourseRegistrationApiController(CandidateService candidateService)
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
        
        [HttpPost("updateDetails")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateContactDetails updateContactDetails)
        {
            var result = await _candidateService.UpdateDetails(updateContactDetails);
            return result ? Json(new {Success = false, Response = "Unknown Server Error"})
                : Json(new {Success = true});
        }

        // [HttpGet("Find")]
        // public async Task<IActionResult> Find()
        // {
        //     var filteredPageRequest = Request.FilteredPageRequest("id", true);
        //     var result = await _exampleService.Find(filteredPageRequest);
        //     return Json(result);
        // }
    }
}