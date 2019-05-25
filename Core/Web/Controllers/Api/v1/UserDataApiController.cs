using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1
{
    [Authorize]
    [Route("/api/v1/user")]
    public class UserDataApiController : RootApiController
    {
        private readonly IUserService _userService;

        private readonly ICandidateService _candidateService;

        public UserDataApiController(IUserService userService, ICandidateService candidateService)
        {
            _userService = userService;
            _candidateService = candidateService;
        }
        
        [HttpGet("getAllCandidates")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _candidateService.GetAll();
            return Json(result);
        }
        
        // [HttpGet("{id:long}")]
        // public async Task<IActionResult> FindCandidateByUserId(long id)
        // {
        //     var result = await _candidateService.FindByUserId(id);
        //     return Json(result);
        // }
        
        // [HttpGet]
        // public async Task<IActionResult> GetCurrentCandidate()
        // {
        //     //TODO this returns the entire row from candidate, FIX!!!!
        //     var result = await _candidateService.FindByUserId();
        //     return Json(result);
        // }
    }
}