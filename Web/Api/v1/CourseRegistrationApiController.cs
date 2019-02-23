using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.v1
{
    [Route("/api/v1/course/register")]
    public class CourseRegistrationApiController : RootApiController
    {
        private readonly ExampleService _exampleService;

        private readonly CandidateService _candidateService;

        public CourseRegistrationApiController(ExampleService exampleService, CandidateService candidateService)
        {
            _exampleService = exampleService;
            _candidateService = candidateService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] Example newEntity)
        {
            await _exampleService.Create(newEntity);
            return Ok();
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find()
        {
            var filteredPageRequest = Request.FilteredPageRequest("id", true);
            var result = await _exampleService.Find(filteredPageRequest);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var result = await _exampleService.FindById(id);
            return Json(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var result = await _candidateService.GenerateReferenceNumber();
            return Json(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Example updatedEntity)
        {
            await _exampleService.Update(id, updatedEntity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _exampleService.Delete(id);
            return Ok();
        }
    }
}