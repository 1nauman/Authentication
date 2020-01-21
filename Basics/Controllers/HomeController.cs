using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var naumanClaims = new[] {
                new Claim(ClaimTypes.Name, "Nauman"),
                new Claim(ClaimTypes.Email, "n.r@live.in"),
                new Claim("Nauman.Claim", "Good one!")
            };
            
            var aadhaarClaims = new[] { new Claim("aadhaar.number", "ABCDEF"), new Claim("aadhaar.enrollment-date", "2010-01-01") };

            var naumanIdentity = new ClaimsIdentity(naumanClaims, "nauman identity");
            var aadhaarIdentity = new ClaimsIdentity(aadhaarClaims, "aadhaar identity");
            
            var naumanPrincipal = new ClaimsPrincipal(new[] { naumanIdentity, aadhaarIdentity });

            HttpContext.SignInAsync(naumanPrincipal);

            return RedirectToAction("Index");
        }
    }
}