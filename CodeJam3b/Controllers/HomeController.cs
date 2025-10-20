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

        //I like this to automatically force the user to login
        //before they can do anything else, that's great.
        
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

            /* In the above query you order desc by name, but since the movie IDs are also listed
             it might be clearer if you don't include the ids, or if you do then order by those 
            (so it is 1-8).*/

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

        //This functionality is great, I like how you keep it simple and only check for 
        //the movie based on the id, then grab all the information. Well done!

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

            //I like this functionality to add ratings, could there be a way inside your profile
            //view to also see the ratings you've given? For ex
            //var ratings = _db.Ratings.Where(r => r.UserId == userId).ToList();
            
            // ✅ Add to favorites if selected
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

                        /*
                         I'm not sure if this functionality entirely works, I tried to add 2 favorites
                        and ran into a duplicate pk error for "pk_watched". Also, when I add a favorite
                        movie it does not show up in my profile page under favorites. Could this maybe be
                        because the FavId is not being set correctly in the Watched table?
                         */

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
                FavId = _db.Watched.Where(w => w.UserId == userId).Select(w => w.FavId).First(),
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

            //I'm not sure if the above query works, I added a movie as a favorite and then it did not appear under my profile.
            //Could it be because of the relation between favorite and watched?

            //This could also be where you add the ratings and actually output them over just the list of movies they watched
            //var ratings = _db.Ratings.Where(r => r.UserId == userId).ToList();

            ViewData["Username"] = username;
            ViewData["Watched"] = alreadyWatched;

            ViewData["Favs"] = favs;

            return View();
        }

    }
}
 
