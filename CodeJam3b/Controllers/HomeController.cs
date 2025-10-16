using CodeJam3b.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LetterBoxDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/login")]
        public IActionResult Login(string name)
        {

            return View();
        }


        [Route("/dashboard")]
        public IActionResult Dashboard()
        {

            return View();
        }

        [Route("/movie")]
        public IActionResult Movie()
        {

            return View();
        }

        [Route("/profile")]
        public IActionResult Profile()
        {

            return View();
        }

    }
}
