using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUserIdentity _user;
        
        public DashboardController(IUserIdentity user)
        {
            _user = user;
        }
        
        
        public IActionResult Index()
        {
            switch (_user.Role) {
                case UserRole.Admin:
                    
                    return View("Admin");
                default:
                    return View("Candidate");
            }
        }

        public IActionResult CourseRegistration()
        {
            return View();
        }

        public IActionResult UpdateContactDetails()
        {
            return View();
        }
    }
}