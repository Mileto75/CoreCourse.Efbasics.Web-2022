using CoreCourse.Efbasics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreCourse.Efbasics.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //bake a cookie
            //cookieOptions
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            //add to response and add cookieOptions as third parameter
            HttpContext.Response.Cookies.Append("SchoolShop", "I was here!",cookieOptions);
            //session with fictional username
            //session with fictional authentication flag
            HttpContext.Session.SetString("UserName", "BSchmitzie");
            HttpContext.Session.SetInt32("Authenticated", 1);

            return View();
        }

        public IActionResult Privacy()
        {
            //get the cookie from the request
            ViewBag.CookieContent = HttpContext.Request.Cookies["SchoolShop"];
            //delete the cookie after getting the data
            HttpContext.Response.Cookies.Delete("SchoolShop");
            //get the session data
            ViewBag.UserName = HttpContext
                .Session.GetString("UserName");
            ViewBag.Authenticated = HttpContext
                .Session.GetInt32("Authenticated");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}