using System;
using System.Linq;
using CodeJam3b.Models.Movies;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllersWithViews();

        // Add DbContext
        builder.Services.AddDbContext<LetterBoxDbContext>();

        // ✅ Add session services
        builder.Services.AddDistributedMemoryCache(); // Required for session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        // ✅ Enable session middleware BEFORE Authorization
        app.UseSession();

        app.UseAuthorization();

        // ✅ Set default route to Login
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}/{id?}");

        app.Run();
    }
}

// unused code below?


//static void RunCli()
//{
//    var exit = false;
//    do
//    {
//        Console.Write("MoviesDB> ");
//        var input = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
//        if (input.Length == 0) continue;

//        switch (input[0].ToLowerInvariant())
//        {
//            case "quit":
//            case "exit":
//                exit = true;
//                break;
//            case "list_movies":
//                ListMovies();
//                break;
//            case "get_movie":
//                if (input.Length >= 2) GetMovieById(input[1]);
//                else Console.WriteLine("Usage: get_movie <id>");
//                break;
//            case "list_users":
//                ListUsers();
//                break;
//            //case "get_user":
//            //    if (input.Length >= 2) GetUserById(input[1]);
//            //    else Console.WriteLine("Usage: get_user <user_id>");
//            //    break;
//            case "add_movie":
//                // add_movie <id> <name> <releaseYear> <genre> <durationMins> <avgRating>
//                if (input.Length >= 7)
//                {
//                    AddMovie(input[1], input[2], input[3], input[4], input[5], input[6]);
//                }
//                else
//                {
//                    Console.WriteLine("Usage: add_movie <id> <name> <releaseYear> <genre> <durationMins> <avgRating>");
//                }
//                break;
//            default:
//                Console.WriteLine("Unknown command. Available: list_movies, get_movie, list_users, get_user, add_movie, quit");
//                break;
//        }

//    } while (!exit);
//}

//static void ListMovies()
//{
//    using (var db = new LetterBoxDbContext())
//    {
//        var movies = db.Movies.OrderBy(m => m.Name).Take(50).ToList();
//        foreach (var m in movies)
//        {
//            Console.WriteLine($"{m.Id}\t{m.Name}\t{m.ReleaseYear}\t{m.Genre}");
//        }
//    }
//}

//static void GetMovieById(string id)
//{
//    using (var db = new LetterBoxDbContext())
//    {
//        var movie = db.Movies.FirstOrDefault(m => m.Id == id);
//        if (movie == null)
//        {
//            Console.WriteLine("Movie not found.");
//            return;
//        }

//        Console.WriteLine($"Id: {movie.Id}");
//        Console.WriteLine($"Name: {movie.Name}");
//        Console.WriteLine($"Year: {movie.ReleaseYear}");
//        Console.WriteLine($"Genre: {movie.Genre}");
//        Console.WriteLine($"Duration: {movie.DurationMins}");
//        Console.WriteLine($"AvgRating: {movie.AvgRating}");
//    }
//}

//static void ListUsers()
//{
//    using (var db = new LetterBoxDbContext())
//    {
//        var users = db.Users.OrderBy(u => u.Username).Take(50).ToList();
//        foreach (var u in users)
//        {
//            Console.WriteLine($"{u.UserId}\t{u.Username}\t{u.Email}");
//        }
//    }
//}

//static void GetUserById(string userId)
//{
//    using (var db = new LetterBoxDbContext())
//    {
//        // userId here is the string 'id' column or the GUID user_id? We'll try matching both.
//        CodeJam3b.Models.Users.User? user = null;
//        if (Guid.TryParse(userId, out var guid))
//        {
//            user = db.Users.FirstOrDefault(u => u.UserId == guid);
//        }
//        if (user == null)
//        {
//            user = db.Users.FirstOrDefault(u => u.Id == userId);
//        }

//        if (user == null)
//        {
//            Console.WriteLine("User not found.");
//            return;
//        }

//        Console.WriteLine($"UserId: {user.UserId}");
//        Console.WriteLine($"Id: {user.Id}");
//        Console.WriteLine($"Username: {user.Username}");
//        Console.WriteLine($"Email: {user.Email}");
//        Console.WriteLine($"Bio: {user.Bio}");
//    }
//}

//    static void AddMovie(string id, string name, string releaseYearRaw, string genre, string durationRaw, string avgRaw)
//    {
//        using (var db = new LetterBoxDbContext())
//        {
//            var movie = new CodeJam3b.Models.Movies.Movie
//            {
//                Id = id,
//                Name = name,
//                Genre = genre
//            };

//            if (int.TryParse(releaseYearRaw, out var ry)) movie.ReleaseYear = ry;
//            if (int.TryParse(durationRaw, out var dm)) movie.DurationMins = dm;
//            if (double.TryParse(avgRaw, out var ar)) movie.AvgRating = ar;

//            db.Movies.Add(movie);
//            db.SaveChanges();

//            Console.WriteLine($"Added movie {movie.Name} ({movie.Id})");
//        }
//    }
//}
