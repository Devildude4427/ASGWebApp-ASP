using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers.Api.v1
{
    [Route("/api/v1/users")]
    [Authorize("Admin")]
    public class UserApiController : RootApiController
    {
        private readonly UserService _userService;

        public UserApiController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var filteredPageRequest = Request.FilteredPageRequest("id", true);
            var result = await _userService.Find(filteredPageRequest);
            return Json(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> FindById(long id)
        {
            var result = await _userService.FindById(id);
            return Json(result);
        }
    }
}