using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.v1
{
    [Route("/api/v1/users")]
    // [Authorize("Admin")]
    public class UserApiController : RootApiController
    {
        private readonly UserService _userService;

        private readonly CandidateService _candidateService;

        public UserApiController(UserService userService, CandidateService candidateService)
        {
            _userService = userService;
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var filteredPageRequest = Request.FilteredPageRequest("id", true);
            var result = await _userService.Find(filteredPageRequest);
            return Json(result);
        }

        // [HttpGet("{id:long}")]
        // public async Task<IActionResult> FindById(long id)
        // {
        //     var result = await _userService.FindById(id);
        //     return Json(result);
        // }
        
        [HttpGet("{id:long}")]
        public async Task<IActionResult> FindCandidateByUserId(long id)
        {
            var result = await _candidateService.FindByUserId(id);
            return Json(result);
        }
    }
}