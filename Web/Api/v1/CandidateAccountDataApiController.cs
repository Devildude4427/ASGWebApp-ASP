using System;
using System.Threading.Tasks;
using Domain.Entities;
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
            await _candidateService.Register(courseRegistration);
            return Ok();
        }
        
        [HttpPost("updateDetails")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateContactDetails updateContactDetails)
        {
            // await _candidateService.UpdateDetails(courseRegistration);
            Console.WriteLine(updateContactDetails.CompanyName);
            return Ok();
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