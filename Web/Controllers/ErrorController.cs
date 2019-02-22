using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int id)
        {
            switch (id)
            {
                case(401):
                case(403):
                    return Redirect("/login");
                case(404):
                    return View("404");
                default:
                    return View("404");
            }
        }
    }
}