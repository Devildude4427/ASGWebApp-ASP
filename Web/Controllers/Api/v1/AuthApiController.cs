using System.Threading.Tasks;
using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.Auth;

namespace Web.Controllers.Api.v1
{
    [Authorize]
    [Route("/api/v1/auth")]
    public class AuthApiController : RootApiController
    {
        private readonly AuthService _authService;
        
        private readonly IUserIdentity _user;

        public AuthApiController(AuthService authService, IUserIdentity user)
        {
            _authService = authService;
            _user = user;
        }

        [HttpGet("session")]
        public IActionResult GetSession()
        {
            return Json(new SessionData
            {
                User = _user
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (_user.IsAuthenticated)
                return Json(new { Success = false, Response = LoginResponse.AlreadyLoggedIn});

            var result = await _authService.Login(loginRequest.Email, loginRequest.Password);

            if (result.LoginResponse != LoginResponse.Successful)
                return Json(new {Success = false, Response = result.LoginResponse, result.User?.Email});
            
            // Response.WithCredentials(result.User);
            return Json(new {Success = true, result});
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.DeleteCredentials();
            return Json(new {Success = true});
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await _authService.Register(registrationRequest);
            return result.LoginResponse != LoginResponse.Successful ? Json(new {Success = false, Response = result.UserRegistrationResponse})
                : Json(new {Success = true});
        }
        
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(long id, [FromBody] Example updatedEntity)
        // {
        //     await _exampleService.Update(id, updatedEntity);
        //     return Ok();
        // }
    }

    public class SessionData
    {
        public IUserIdentity User { get; set; }
    }
}