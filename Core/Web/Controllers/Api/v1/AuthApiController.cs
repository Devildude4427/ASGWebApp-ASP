using System.Security.Claims;
using System.Threading.Tasks;
using Core.Domain.Models.Authentication;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers.Api.v1
{
    [Route("/api/v1/auth")]
    public class AuthApiController : RootApiController
    {
        private readonly IAuthService _authService;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (User.FindFirstValue(ClaimTypes.Email) != null)
                return Json(new { Success = false, Response = LoginResponse.AlreadyLoggedIn.ToString()});
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Response = LoginResponse.IncompleteDetails.ToString()});
            }
            
            var result = await _authService.Login(loginRequest);
            return result.LoginResponse != LoginResponse.Successful
                ? Json(new {Success = false, Response = result.LoginResponse.ToString()})
                : Json(new {Success = true, result.JwtToken});
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await _authService.Register(registrationRequest);
            return result.UserRegistrationResponse != UserRegistrationResponse.Successful
                ? Json(new {Success = false, Response = result.UserRegistrationResponse.ToString()})
                : Json(new {Success = true});
        }
    }
}