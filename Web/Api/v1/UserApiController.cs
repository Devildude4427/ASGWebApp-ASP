using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Api.v1
{
    [Route("/api/v1/user")]
    public class UserApiController : RootApiController
    {
        private readonly UserService _userService;

        private readonly CandidateService _candidateService;
        
        private readonly IUserIdentity _user;

        public UserApiController(UserService userService, CandidateService candidateService, IUserIdentity user)
        {
            _userService = userService;
            _candidateService = candidateService;
            _user = user;
        }

        // [HttpGet]
        // public async Task<IActionResult> Find()
        // {
        //     var filteredPageRequest = Request.FilteredPageRequest("id", true);
        //     var result = await _userService.Find(filteredPageRequest);
        //     return Json(result);
        // }
        
        [HttpGet("{id:long}")]
        public async Task<IActionResult> FindCandidateByUserId(long id)
        {
            var result = await _candidateService.FindByUserId(id);
            return Json(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCurrentCandidate()
        {
            var result = await _candidateService.FindByUserId(_user.Id);
            return Json(result);
        }
    }
}