using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = new User {Name = "Nancy Davolino"};
            ViewData["heading"] = "Welcome to ASP.NET Core MVC !!";
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}