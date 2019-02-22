using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserIdentity _user;
        
        public DashboardController(IUserIdentity user)
        {
            _user = user;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            switch (_user.Role) {
                case UserRole.Admin:
                    return View("Admin");
                default:
                    return View("Candidate");
            }
        }
    }
}