using System;
using Domain.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public void Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine(loginRequest.Email);
            Console.WriteLine(loginRequest.Password);
            Redirect("Home/Index");
        }
    }
}