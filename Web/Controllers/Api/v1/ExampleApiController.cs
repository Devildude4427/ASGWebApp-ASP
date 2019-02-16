using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers.Api.v1
{
    [Route("/api/v1/examples")]
    public class ExampleApiController : RootApiController
    {
        private readonly ExampleService _exampleService;

        public ExampleApiController(ExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] Example newEntity)
        {
            await _exampleService.Create(newEntity);
            return Ok();
        }

        [HttpGet("")]
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