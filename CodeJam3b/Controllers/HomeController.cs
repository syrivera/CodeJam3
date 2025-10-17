using CodeJam3b.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LetterBoxDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LetterBoxDbContext _db;

        public HomeController(ILogger<HomeController> logger, LetterBoxDbContext db)
        {
            _logger = logger;
            _db = db;
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

        public IActionResult Dashboard(string? q, int page = 1)
        {

            const int pageSize = 20;
            q = (q ?? string.Empty).Trim();
            page = page < 1 ? 1 : page;

            IQueryable<Movie> query = _db.Movies.AsNoTracking();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                    .Where(m => m.Name != null && EF.Functions.ILike(m.Name!, $"%{q}%"))
                    .OrderByDescending(m => EF.Functions.ILike(m.Name!, $"{q}%"))
                    .ThenBy(m => m.Name);
            }
            else
            {
                query = query.OrderBy(m => m.Name!);
            }

            int totalCount = query.Count();

            List<Movie> movies = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["Query"] = q;
            ViewData["Page"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewData["TotalCount"] = totalCount;

            return View(movies);



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
