using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyAuthJWT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Route("/")]
        [Authorize(Policy = "HomeRead")]
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        [Route("/About")]
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        [Route("/Privacy")]
        public IActionResult Privacy()
        {
            ViewBag.Title = "Privacy";
            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }
    }
}
