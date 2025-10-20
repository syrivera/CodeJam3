using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CodeJam3b.Models.Users;
using CodeJam3b.Models.Lists;

namespace CodeJam3b.Models.Movies;

public class LetterBoxDbContext(string dbName = "letterbox") : DbContext
{
    // dont think these are being used
    private readonly string _connectionHost = "localhost";
    private readonly string _connectionDbName = dbName;

        // YOUR MODEL TYPES MUST EXIST IN THE PROJECT
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Watchlist> Watchlist { get; set; } = null!;
        public DbSet<Watched> Watched { get; set; } = null!;
        public DbSet<Fav> Fav { get; set; } = null!;
        public DbSet<Diary> Diary { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // maybe dont hardcode credentials (I probably did too lol)
        optionsBuilder.UseNpgsql(connectionString:
            "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=letterbox;Include Error Detail=true;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Movies
        var movies = new[]
        {
            new Movie { Id = "movie-001", Name = "The Shawshank Redemption", ReleaseYear = 1994, Genre = "Drama", DurationMins = 142, AvgRating = 4.8 },
            new Movie { Id = "movie-002", Name = "The Godfather", ReleaseYear = 1972, Genre = "Crime", DurationMins = 175, AvgRating = 4.7 },
            new Movie { Id = "movie-003", Name = "Inception", ReleaseYear = 2010, Genre = "Sci-Fi", DurationMins = 148, AvgRating = 4.5 },
            new Movie { Id = "movie-004", Name = "Pulp Fiction", ReleaseYear = 1994, Genre = "Crime", DurationMins = 154, AvgRating = 4.6 },
            new Movie { Id = "movie-005", Name = "The Matrix", ReleaseYear = 1999, Genre = "Sci-Fi", DurationMins = 136, AvgRating = 4.4 },
            new Movie { Id = "movie-006", Name = "Interstellar", ReleaseYear = 2014, Genre = "Sci-Fi", DurationMins = 169, AvgRating = 4.3 },
            new Movie { Id = "movie-007", Name = "Fight Club", ReleaseYear = 1999, Genre = "Drama", DurationMins = 139, AvgRating = 4.5 },
            new Movie { Id = "movie-008", Name = "Forrest Gump", ReleaseYear = 1994, Genre = "Drama", DurationMins = 142, AvgRating = 4.6 }
        };
        modelBuilder.Entity<Movie>().HasData(movies);

        // Seed Users
        var users = new[]
        {
            new User
            {
                UserId = "user-001",
                Username = "moviebuff123",
                Email = "john@example.com",
                Bio = "Love classic films and sci-fi",
                DiaryId = "diary-001",
                WatchlistId = "watchlist-001",
                WatchedId = "watched-001"
            },
            new User
            {
                UserId = "user-002",
                Username = "cinephile",
                Email = "sarah@example.com",
                Bio = "Drama enthusiast and film critic",
                DiaryId = "diary-002",
                WatchlistId = "watchlist-002",
                WatchedId = "watched-002"
            },
            new User
            {
                UserId = "user-003",
                Username = "filmfanatic",
                Email = "mike@example.com",
                Bio = "Watching everything from classics to modern masterpieces",
                DiaryId = "diary-003",
                WatchlistId = "watchlist-003",
                WatchedId = "watched-003"
            }
        };
        modelBuilder.Entity<User>().HasData(users);

        // Seed Ratings
        var ratings = new[]
        {
            // User 1 ratings
            new Rating { Id = "rating-001", RatingId =  "rating-001", UserId = "user-001", MovieName = "The Shawshank Redemption", Stars = 5, Review = "Absolute masterpiece! Tim Robbins and Morgan Freeman deliver unforgettable performances." },
            new Rating { Id = "rating-002", RatingId =  "rating-002", UserId = "user-001", MovieName = "Inception", Stars = 5, Review = "Mind-bending brilliance. Nolan at his best!" },
            new Rating { Id = "rating-003", RatingId =  "rating-003", UserId = "user-001", MovieName = "The Matrix", Stars = 4, Review = "Revolutionary sci-fi that still holds up today." },
            
            // User 2 ratings
            new Rating { Id = "rating-004", RatingId = "rating-004", UserId = "user-002", MovieName = "The Godfather", Stars = 5, Review = "The pinnacle of cinema. Every scene is perfection." },
            new Rating { Id = "rating-005", RatingId = "rating-005", UserId = "user-002", MovieName = "Pulp Fiction", Stars = 4, Review = "Tarantino's masterclass in non-linear storytelling." },
            new Rating { Id = "rating-006", RatingId = "rating-006", UserId = "user-002", MovieName = "Fight Club", Stars = 5, Review = "Dark, twisted, and absolutely brilliant." },
            
            // User 3 ratings
            new Rating { Id = "rating-007", RatingId = "rating-007", UserId = "user-003", MovieName = "Interstellar", Stars = 4, Review = "Visually stunning with an emotional core." },
            new Rating { Id = "rating-008", RatingId = "rating-008", UserId = "user-003", MovieName = "Forrest Gump", Stars = 5, Review = "Heartwarming journey through American history." },
            new Rating { Id = "rating-009", RatingId = "rating-009", UserId = "user-003", MovieName = "The Shawshank Redemption", Stars = 5, Review = "Hope is a good thing, maybe the best of things." }
        };
        modelBuilder.Entity<Rating>().HasData(ratings);

        // Seed Diary entries (users' movie journals with reviews)
        var diaryEntries = new[]
        {
            // User 1 diary entries
            new Diary {Id = "1", DiaryId = "diary-001", MovieId = "movie-001", RatingId = "rating-001" },
            new Diary {Id = "2",  DiaryId = "diary-001", MovieId = "movie-003", RatingId = "rating-002" },
            new Diary {Id = "3",  DiaryId = "diary-001", MovieId = "movie-005", RatingId = "rating-003" },
            
            // User 2 diary entries
            new Diary {Id = "4",  DiaryId = "diary-002", MovieId = "movie-002", RatingId = "rating-004" },
            new Diary {Id = "5",  DiaryId = "diary-002", MovieId = "movie-004", RatingId = "rating-005" },
            new Diary {Id = "6",  DiaryId = "diary-002", MovieId = "movie-007", RatingId = "rating-006" },
            
            // User 3 diary entries
            new Diary {Id = "7",  DiaryId = "diary-003", MovieId = "movie-006", RatingId = "rating-007" },
            new Diary {Id = "8",  DiaryId = "diary-003", MovieId = "movie-008", RatingId = "rating-008" },
            new Diary {Id = "9",  DiaryId = "diary-003", MovieId = "movie-001", RatingId = "rating-009" }
        };
        modelBuilder.Entity<Diary>().HasData(diaryEntries);

        // Seed Favorites
        var favorites = new[]
        {
            // User 1 favorites
            new Fav {Id = "1", FavId = "fav-001", MovieId = "movie-001" },  // Shawshank
            new Fav {Id = "2", FavId = "fav-001", MovieId = "movie-003" },  // Inception
            
            // User 2 favorites
            new Fav {Id = "3", FavId = "fav-002", MovieId = "movie-002" },  // Godfather
            new Fav {Id = "4",  FavId = "fav-002", MovieId = "movie-007" },  // Fight Club
            
            // User 3 favorites
            new Fav {Id = "5",  FavId = "fav-003", MovieId = "movie-008" },  // Forrest Gump
            new Fav {Id = "6",  FavId = "fav-003", MovieId = "movie-001" }   // Shawshank
        };
        modelBuilder.Entity<Fav>().HasData(favorites);

        // Seed Watched list (movies users have seen)
        var watchedEntries = new[]
        {
            // User 1 watched
            new Watched { Id = "watched-entry-001", UserId = "user-001", MovieId = "movie-001", FavId = "fav-001", DiaryId = "diary-001" },
            new Watched { Id = "watched-entry-002", UserId = "user-001", MovieId = "movie-003", FavId = "fav-001", DiaryId = "diary-001" },
            new Watched { Id = "watched-entry-003", UserId = "user-001", MovieId = "movie-005", DiaryId = "diary-001" },
            new Watched { Id = "watched-entry-004", UserId = "user-001", MovieId = "movie-002" },
            
            // User 2 watched
            new Watched { Id = "watched-entry-005", UserId = "user-002", MovieId = "movie-002", FavId = "fav-002", DiaryId = "diary-002" },
            new Watched { Id = "watched-entry-006", UserId = "user-002", MovieId = "movie-004", DiaryId = "diary-002" },
            new Watched { Id = "watched-entry-007", UserId = "user-002", MovieId = "movie-007", FavId = "fav-002", DiaryId = "diary-002" },
            new Watched { Id = "watched-entry-008", UserId = "user-002", MovieId = "movie-001" },
            
            // User 3 watched
            new Watched { Id = "watched-entry-009", UserId = "user-003", MovieId = "movie-006", DiaryId = "diary-003" },
            new Watched { Id = "watched-entry-010", UserId = "user-003", MovieId = "movie-008",  FavId = "fav-003", DiaryId = "diary-003" },
            new Watched { Id = "watched-entry-011", UserId = "user-003", MovieId = "movie-001", FavId = "fav-003", DiaryId = "diary-003" }
        };
        modelBuilder.Entity<Watched>().HasData(watchedEntries);

        // Seed Watchlist (movies users want to watch)
        var watchlistEntries = new[]
        {
            // User 1 wants to watch
            new Watchlist {Id = "1",  MovieId = "movie-004", WatchlistId = "watchlist-001" },  // Pulp Fiction
            new Watchlist {Id = "2", MovieId = "movie-006", WatchlistId = "watchlist-001" },  // Interstellar
            new Watchlist { Id = "3", MovieId = "movie-007", WatchlistId = "watchlist-001" },  // Fight Club
            
            // User 2 wants to watch
            new Watchlist { Id = "4", MovieId = "movie-003", WatchlistId = "watchlist-002" },  // Inception
            new Watchlist { Id = "5",MovieId = "movie-005", WatchlistId = "watchlist-002" },  // Matrix
            new Watchlist {Id = "6",MovieId = "movie-006", WatchlistId = "watchlist-002" },  // Interstellar
            
            // User 3 wants to watch
            new Watchlist {Id = "7",  MovieId = "movie-002", WatchlistId = "watchlist-003" },  // Godfather
            new Watchlist {Id = "8",  MovieId = "movie-004", WatchlistId = "watchlist-003" },  // Pulp Fiction
            new Watchlist { Id = "9", MovieId = "movie-007", WatchlistId = "watchlist-003" }   // Fight Club
        };
        modelBuilder.Entity<Watchlist>().HasData(watchlistEntries);
    }
}