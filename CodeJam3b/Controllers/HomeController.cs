using CodeJam3b.Models.Lists;
using CodeJam3b.Models.Movies;
using CodeJam3b.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        [Route("/")]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        
        [Route("/login")]
        public IActionResult Login(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == name);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId!);
                HttpContext.Session.SetString("Username", user.Username!);
                return RedirectToAction("Dashboard");
            }

            ViewData["Error"] = "Invalid username";
            return View();
        }

        [Route("/dashboard")]
        public IActionResult Dashboard(string? q, int page = 1)
        {
            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

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

            var movies = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["Username"] = username;
            ViewData["UserId"] = userId;
            ViewData["Query"] = q;
            ViewData["Page"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewData["TotalCount"] = totalCount;

            return View(movies);
        }

        [Route("/movie/{id}")]
        public IActionResult Movie(string id)
        {
            var movie = _db.Movies
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            ViewData["Title"] = movie.Name;
            return View(movie);
        }

        // - This route combines both the add to favorites functionality and the submit review.
        // - Should separate the two so a user can access each one individually
        // - This method also has the logic for marking a movie as watched, this could be a separate method as well
        [HttpPost]
        [Route("/movie/{id}/submit")]
        public IActionResult SubmitReview(string id, int rating, string? review, bool isFavorite = false)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            // ✅ Get the last Rating ID (string like "rating-001") and increment
            var lastRating = _db.Ratings
                .OrderByDescending(r => r.Id)
                .FirstOrDefault();

            int lastNum = 0;
            if (lastRating != null && lastRating.Id.StartsWith("rating-"))
            {
                var parts = lastRating.Id.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int parsed))
                {
                    lastNum = parsed;
                }
            }

            string nextRatingId = $"rating-{(lastNum + 1):D3}";

            var newRating = new Rating
            {
                Id = nextRatingId,
                RatingId = nextRatingId,
                UserId = userId,
                MovieName = movie.Name!,
                Review = review,
                Stars = rating
            };

            _db.Ratings.Add(newRating);

            // ✅ Add to favorites if selected

            // The user table does not have a "fav_id" column in the EntityFramework model, but it is shown on the ERD
            // Need to implement the logic to tie users to their favorite movies

            if (isFavorite)
            {
                var watched = _db.Watched.FirstOrDefault(w => w.UserId == userId);
                if (watched != null && !string.IsNullOrEmpty(watched.FavId))
                {
                    // Avoid duplicates
                    bool alreadyFavorite = _db.Fav.Any(f => f.MovieId == movie.Id && f.FavId == watched.FavId);
                    if (!alreadyFavorite)
                    {
                        // Get last Fav.Id (numeric string), increment it
                        var lastFav = _db.Fav
                            .OrderByDescending(f => f.Id)
                            .FirstOrDefault();

                        int lastFavId = 0;
                        if (lastFav != null && int.TryParse(lastFav.Id.ToString(), out int parsedFavId))
                        {
                            lastFavId = parsedFavId;
                        }

                        var fav = new Fav
                        {
                            Id = (lastFavId + 1).ToString(),  // stored as string
                            FavId = watched.FavId,
                            MovieId = movie.Id
                        };

                        _db.Fav.Add(fav);
                        
                    }
                }
            }

            var lastRatingWatched = _db.Watched
               .OrderByDescending(r => r.Id)
               .FirstOrDefault();

            int lastNumWatched = 0;
            if (lastRatingWatched != null && lastRatingWatched.Id.StartsWith("watched-"))
            {
                var partsWatched = lastRatingWatched.Id.Split('-');
                if (partsWatched.Length == 2 && int.TryParse(partsWatched[1], out int parsedWatched))
                {
                    lastNumWatched = parsedWatched;
                }
            }
            string nextWatchedId = $"watched-{(lastNumWatched + 1):D3}";

            var newWatched = new Watched
            {
                Id = nextWatchedId,

                // Consider adding "FirstOrDefault()" instead of First() - consistent with rest of file and better error handling
                // This FavId setter will not work unless the column was added as a foreign key to the users table

                FavId = _db.Watched.Where(w => w.UserId == userId).Select(w => w.FavId).FirstOrDefault(), // Always NULL

                // FavId = _db.Watched.Where(w => w.UserId == userId).Select(w => w.FavId).First(), 
                UserId = userId,
                MovieId = id,
                DiaryId = id
            };
            Console.WriteLine(_db.Watched.Where(w => w.UserId == userId).Select(w => w.FavId).FirstOrDefault());

            _db.Watched.Add(newWatched);

            _db.SaveChanges();

            TempData["SuccessMessage"] = "Thanks for your review!";
            return RedirectToAction("Dashboard");
        }

        [Route("/profile")]
        public IActionResult Profile(string id)
        {
            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetString("UserId");
            var watched = _db.Watched.FirstOrDefault(w => w.UserId == userId);
            var movies = _db.Movies.FirstOrDefault();

            if (username == null)
            {
                ViewData["Error"] = "User not found.";
                return View();
            }

            var alreadyWatched = _db.Watched.Where(w => w.UserId == userId).ToList().Select(w => _db.Movies.FirstOrDefault(m => m.Id == w.MovieId).Name)
                .Where(m => m != null)
                .ToList();

            var favs = _db.Fav.Where(f => f.FavId == watched.FavId).ToList().Select(w => _db.Movies.FirstOrDefault(m => m.Id == w.MovieId).Name)
                .Where(m => m != null)
                .ToList();

            ViewData["Username"] = username;
            ViewData["Watched"] = alreadyWatched;

            ViewData["Favs"] = favs;

            return View();
        }

    }
}
 
