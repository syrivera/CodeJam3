using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Diagnostics;
using CodeJam3b.Models.Users;
using CodeJam3b.Models.Lists;

namespace CodeJam3b.Models.Movies
{
    public class LetterBoxDbContext : DbContext
    {
        private readonly string _connectionHost = "localhost";
        private readonly string _connectionDbName;

        public LetterBoxDbContext() : this("letterbox") { }

        public LetterBoxDbContext(string dbName)
        {
            _connectionDbName = dbName ?? "letterbox";
        }

        public LetterBoxDbContext(DbContextOptions<LetterBoxDbContext> options) : base(options)
        {
            // When options are provided, we won't override them in OnConfiguring.
            _connectionDbName = "letterbox";
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Watchlist> Watchlist { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<Fav> Fav { get; set; }
        public DbSet<Diary> Diary { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={_connectionHost};Database={_connectionDbName}");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CodeJam3b.Models.Lists.Watched>()
                .HasOne<CodeJam3b.Models.Users.User>()
                .WithMany(u => u.Watched)
                .HasForeignKey(w => w.UserId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Diary.RatingId (int) maps to Rating.RatingId (int) - configure explicitly
            modelBuilder.Entity<CodeJam3b.Models.Lists.Diary>()
                .HasOne<CodeJam3b.Models.Movies.Rating>(d => d.Rating)
                .WithMany()
                .HasForeignKey(d => d.RatingId)
                .HasPrincipalKey(r => r.RatingId)
                .OnDelete(DeleteBehavior.SetNull);

            // Rating.UserId (string) maps to User.UserId (string) - configure explicitly
            modelBuilder.Entity<CodeJam3b.Models.Movies.Rating>()
                .HasOne<CodeJam3b.Models.Users.User>(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public void SeedFromCsv(string moviesCsvPath, string genresCsvPath)
        {
            if (!System.IO.File.Exists(moviesCsvPath)) throw new System.IO.FileNotFoundException(moviesCsvPath);
            if (!System.IO.File.Exists(genresCsvPath)) throw new System.IO.FileNotFoundException(genresCsvPath);

            var genres = System.IO.File.ReadAllLines(genresCsvPath).Skip(1).Select(l => l.Trim()).Where(l => l.Length > 0).ToArray();
            var movieLines = System.IO.File.ReadLines(moviesCsvPath).Skip(1).Where(l => l.Trim().Length > 0);

            const int batchSize = 10000;
            var batch = new List<Movie>(batchSize);
            long processed = 0;
            foreach (var line in movieLines)
            {
                var parts = SplitCsvLine(line);
                if (parts.Length < 6) continue;
                var id = parts[0].Trim();
                // check existence in DB by primary key quickly
                if (this.Movies.Any(m => m.Id == id)) { processed++; continue; }
                var movie = new Movie
                {
                    Id = id,
                    Name = parts[1].Trim(),
                    ReleaseYear = int.TryParse(parts[2], out var ry) ? ry : (int?)null,
                    Genre = parts[3].Trim(),
                    DurationMins = int.TryParse(parts[4], out var dm) ? dm : (int?)null,
                    AvgRating = double.TryParse(parts[5], out var ar) ? ar : (double?)null
                };
                batch.Add(movie);
                processed++;

                if (batch.Count >= batchSize)
                {
                    this.Movies.AddRange(batch);
                    this.SaveChanges();
                    this.ChangeTracker.Clear();
                    Console.WriteLine($"Inserted {processed:N0} movies...");
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                this.Movies.AddRange(batch);
                this.SaveChanges();
                this.ChangeTracker.Clear();
                Console.WriteLine($"Inserted {processed:N0} movies (final batch).");
                batch.Clear();
            }

            var users = new List<CodeJam3b.Models.Users.User>();
            var rnd = new Random(123);
            int userCounter = 1, diaryCounter = 1, watchlistCounter = 1, favCounter = 1, watchedCounter = 1, ratingCounter = 1;
            for (int i = 1; i <= 10; i++)
            {
                string username, bio;
                switch (i) {
                    case 1:
                        username = "Sam";
                        bio = "Film buff, coffee addict, and aspiring screenwriter. Always searching for the next mind-bending thriller.";
                        break;
                    case 2:
                        username = "Jordan";
                        bio = "Jordan loves indie films, hiking, and spontaneous road trips. Can quote Tarantino at will.";
                        break;
                    case 3:
                        username = "Kristian";
                        bio = "Kristian: Animation enthusiast, board game champion, and collector of obscure movie trivia.";
                        break;
                    case 4:
                        username = "user4";
                        bio = "Late-night movie watcher. Believes popcorn is a food group.";
                        break;
                    case 5:
                        username = "user5";
                        bio = "Cinephile with a soft spot for 80s soundtracks and epic fantasy sagas.";
                        break;
                    case 6:
                        username = "user6";
                        bio = "Documentary lover, amateur photographer, and festival regular.";
                        break;
                    case 7:
                        username = "user7";
                        bio = "Always up for a rom-com marathon. Writes reviews with too many emojis.";
                        break;
                    case 8:
                        username = "user8";
                        bio = "Action junkie, sci-fi dreamer, and self-proclaimed movie quote master.";
                        break;
                    case 9:
                        username = "user9";
                        bio = "Enjoys foreign films, spicy food, and debating plot twists.";
                        break;
                    case 10:
                        username = "user10";
                        bio = "Classic cinema devotee. Can recite Casablanca by heart.";
                        break;
                    default:
                        username = $"user{i}";
                        bio = $"Movie fan.";
                        break;
                }
                var u = new CodeJam3b.Models.Users.User
                {
                    UserId = $"Uid{userCounter}",
                    Username = username,
                    Email = $"user{i}@example.com",
                    Bio = bio
                };
                users.Add(u);
                this.Users.Add(u);
                userCounter++;
            }

            this.SaveChanges();

            // Re-query movies from DB for random selection
            var allMovies = this.Movies.AsNoTracking().ToList();

            string[] sampleReviews = new[] {
                "A visually stunning masterpiece with a moving story.",
                "Solid performances but the plot was ELECTRIC.",
                "Absolutely loved the soundtrack and cinematography!",
                "A bit too long, but the ending was worth it.",
                "Not my cup of tea, but I appreciate the effort.",
                "A thrilling ride from start to finish.",
                "Characters felt real and relatable.",
                "The pacing dragged in the middle.",
                "A must-watch for fans of the genre.",
                "Left the theater with a smile!",
                "Disappointing sequel to a great original.",
                "The humor really landed for me.",
                "A bold and creative take on a classic story.",
                "The visuals outshined the script.",
                "A heartwarming and emotional journey.",
                "Too many plot holes to ignore.",
                "The cast had great chemistry.",
                "A forgettable experience overall.",
                "Exceeded my expectations!",
                "Would definitely watch again."
            };

            foreach (var user in users)
            {
                int rcount = rnd.Next(3, 8);
                for (int j = 0; j < rcount; j++)
                {
                    var mv = allMovies[rnd.Next(allMovies.Count)];
                    var rating = new CodeJam3b.Models.Movies.Rating
                    {
                        Id = $"Rid{ratingCounter}",
                        RatingId = $"Rid{ratingCounter}",
                        UserId = user.UserId,
                        MovieName = mv.Name,
                        Review = sampleReviews[rnd.Next(sampleReviews.Length)],
                        Stars = rnd.Next(1, 6)
                    };
                    this.Ratings.Add(rating);
                    ratingCounter++;

                    if (rnd.NextDouble() < 0.4)
                    {
                        var diary = new CodeJam3b.Models.Lists.Diary
                        {
                            DiaryId = $"Did{diaryCounter}",
                            MovieId = mv.Id,
                            RatingId = rating.RatingId
                        };
                        this.Diary.Add(diary);
                        user.DiaryId = diary.DiaryId;
                        diaryCounter++;
                    }
                }

                int wcount = rnd.Next(1, 5);
                for (int j = 0; j < wcount; j++)
                {
                    var mv = allMovies[rnd.Next(allMovies.Count)];
                    var wl = new CodeJam3b.Models.Lists.Watchlist
                    {
                        WatchlistId = $"Wid{watchlistCounter}",
                        Id = $"Wid{watchlistCounter}",
                        MovieId = mv.Id
                    };
                    this.Watchlist.Add(wl);
                    user.WatchlistId = wl.WatchlistId;
                    watchlistCounter++;
                }

                int wch = rnd.Next(1, 8);
                for (int j = 0; j < wch; j++)
                {
                    var mv = allMovies[rnd.Next(allMovies.Count)];
                    var watched = new CodeJam3b.Models.Lists.Watched
                    {
                        Id = $"Zid{watchedCounter}",
                        FavId = null,
                        DiaryId = user.DiaryId,
                        UserId = user.UserId,
                        MovieId = mv.Id
                    };
                    this.Watched.Add(watched);
                    watchedCounter++;
                }
            }

            foreach (var mv in allMovies.OrderBy(m => m.Id).Take(20))
            {
                var fav = new CodeJam3b.Models.Lists.Fav
                {
                    FavId = $"Fid{favCounter}",
                    MovieId = mv.Id
                };
                this.Fav.Add(fav);
                favCounter++;
            }

            this.SaveChanges();
        }

        public void SeedUsers(int count)
        {
            if (count <= 0) return;


            var rnd = new Random(42);
            var handles = new[] { "luna", "atlas", "momo", "echo", "kirby", "nova", "zephyr", "pixel", "maru", "sable", "orion", "cerise", "bay", "marlow", "tink", "poppy", "indigo", "sage", "ember", "finn", "riven", "gale", "kai", "rio", "sol", "blyx", "airo", "vex", "cleo", "quinn", "aria" };

            // Use existing movies if present
            var allMovies = this.Movies.AsNoTracking().ToList();

            // Track existing user 'Id' strings to avoid duplicates
            var existingIds = new HashSet<string>(this.Users.AsNoTracking().Select(u => u.UserId).Where(id => !string.IsNullOrEmpty(id)).Select(id => id!));

            var users = new List<CodeJam3b.Models.Users.User>();
            int userCounter2 = 1, diaryCounter2 = 1, watchlistCounter2 = 1, watchedCounter2 = 1, favCounter2 = 1;
            for (int i = 1; i <= count; i++)
            {
                var baseHandle = handles[(i - 1) % handles.Length];
                string candidateId = baseHandle;
                int suffix = 0;
                while (existingIds.Contains(candidateId))
                {
                    suffix++;
                    candidateId = baseHandle + suffix.ToString();
                }
                existingIds.Add(candidateId);

                var email = (i % 7 == 0) ? null : $"{candidateId}@fictionalmail.test"; // some nulls
                var username = (i % 11 == 0) ? null : candidateId; // occasionally null
                var bio = (i % 5 == 0) ? null : $"Traveler, cinephile and amateur critic. Loves obscure films and late-night pizza. (seed user {i})";

                var u = new CodeJam3b.Models.Users.User
                {
                    UserId = $"Uid{userCounter2}",
                    Username = username,
                    Email = email,
                    Bio = bio,
                    ListId = (i % 4 == 0) ? $"Lid{userCounter2}" : null // some users have a list id
                };

                // Create a watchlist entry for some users
                if (i % 3 != 0)
                {
                    var watchlist = new CodeJam3b.Models.Lists.Watchlist
                    {
                        WatchlistId = $"Wid{watchlistCounter2}",
                        Id = $"Wid{watchlistCounter2}",
                        MovieId = allMovies.Count > 0 ? allMovies[rnd.Next(allMovies.Count)].Id : null
                    };
                    this.Watchlist.Add(watchlist);
                    u.WatchlistId = watchlist.WatchlistId;
                    u.Watchlist = watchlist;
                    watchlistCounter2++;
                }

                // Some users will have a diary and a diary entry
                if (i % 6 != 0 && allMovies.Count > 0)
                {
                    var mv = allMovies[rnd.Next(allMovies.Count)];
                    var diary = new CodeJam3b.Models.Lists.Diary
                    {
                        DiaryId = $"Did{diaryCounter2}",
                        MovieId = mv.Id,
                        RatingId = null
                    };
                    this.Diary.Add(diary);
                    u.DiaryId = diary.DiaryId;
                    u.Diary = diary;
                    diaryCounter2++;
                }

                // Add some watched entries
                int watchedCount = rnd.Next(0, 6);
                for (int w = 0; w < watchedCount; w++)
                {
                    var mvId = allMovies.Count > 0 ? allMovies[rnd.Next(allMovies.Count)].Id : string.Empty;
                    var watched = new CodeJam3b.Models.Lists.Watched
                    {
                        Id = $"Zid{watchedCounter2}",
                        FavId = (rnd.NextDouble() < 0.2) ? $"Fid{favCounter2}" : null,
                        DiaryId = u.DiaryId ?? string.Empty,
                        UserId = u.UserId,
                        MovieId = mvId
                    };
                    this.Watched.Add(watched);
                    u.Watched.Add(watched);
                    watchedCounter2++;
                    favCounter2++;
                }

                users.Add(u);
                this.Users.Add(u);
                userCounter2++;
            }

            // Create some ratings broadly
            foreach (var user in users)
            {
                if (allMovies.Count == 0) break;
                int rcount = rnd.Next(1, 5);
                for (int r = 0; r < rcount; r++)
                {
                    var mv = allMovies[rnd.Next(allMovies.Count)];
                    var rating = new CodeJam3b.Models.Movies.Rating
                    {
                        Id = $"Rid{user.UserId}_{r+1}",
                        RatingId = $"Rid{user.UserId}_{r+1}",
                        UserId = user.UserId,
                        MovieName = mv.Name,
                        Review = (rnd.NextDouble() < 0.2) ? null : GenerateReview(rnd),
                        Stars = rnd.Next(1, 6)
                    };
                    this.Ratings.Add(rating);

                    // Optionally attach rating to diary
                    if (user.DiaryId != null && rnd.NextDouble() < 0.3)
                    {
                        var d = this.Diary.FirstOrDefault(x => x.DiaryId == user.DiaryId);
                        if (d != null) d.RatingId = rating.RatingId;
                    }
                }
            }

            // Create some favs for a selection of movies
            var topMovies = allMovies.OrderBy(m => m.Id).Take(50).ToList();
            foreach (var mv in topMovies)
            {
                var fav = new CodeJam3b.Models.Lists.Fav
                {
                    FavId = ShortId(rnd, 6),
                    MovieId = mv.Id
                };
                this.Fav.Add(fav);
            }

            this.SaveChanges();
        }

        // CSV split helper
        private static string[] SplitCsvLine(string line)
        {
            var parts = new List<string>();
            bool inQuotes = false;
            var cur = new System.Text.StringBuilder();
            foreach (var c in line)
            {
                if (c == '"') { inQuotes = !inQuotes; continue; }
                if (c == ',' && !inQuotes)
                {
                    parts.Add(cur.ToString()); cur.Clear();
                }
                else cur.Append(c);
            }
            parts.Add(cur.ToString());
            return parts.ToArray();
        }

        // Generate a creative review of 1-5 sentences using the provided Random
        private static string GenerateReview(Random rnd)
        {
            var seeds = new[] {
                "A quietly haunting piece that lingers.",
                "Bold direction and thoughtful pacing.",
                "Acting is raw and convincing.",
                "A few scenes sag, but the ending redeems the journey.",
                "Noteworthy cinematography and unusual score.",
                "I laughed more than I expected.",
                "A curious mixture of tenderness and discomfort.",
                "Bright, clever, and oddly touching.",
                "A slow burn that rewards patient viewers.",
                "The narrative felt disjointed in parts but stayed with me afterward."
            };
            int sentences = rnd.Next(1, 6);
            var parts = new List<string>();
            for (int i = 0; i < sentences; i++) parts.Add(seeds[rnd.Next(seeds.Length)]);
            return string.Join(" ", parts);
        }

        // Generate a short alphanumeric id of requested length
        private static string ShortId(Random rnd, int length = 6)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new System.Text.StringBuilder(length);
            for (int i = 0; i < length; i++) sb.Append(chars[rnd.Next(chars.Length)]);
            return sb.ToString();
        }

        // Normalize existing string id columns to shorter ids (6 chars).
        // If apply==false the method only returns a preview of how many rows would change and shows samples.
        public (int favChanged, int diaryChanged, int watchlistChanged, int ratingChanged, List<(string oldId,string newId)> samples) NormalizeExistingIds(bool apply = false, int length = 6)
        {
            var rnd = new Random();
            // Collect current ids
            var favs = this.Fav.AsNoTracking().ToList();
            var diaries = this.Diary.AsNoTracking().ToList();
            var watchlists = this.Watchlist.AsNoTracking().ToList();
            var ratings = this.Ratings.AsNoTracking().ToList();

            var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            used.UnionWith(favs.Where(f => !string.IsNullOrWhiteSpace(f.FavId)).Select(f => f.FavId));
            used.UnionWith(diaries.Where(d => !string.IsNullOrWhiteSpace(d.DiaryId)).Select(d => d.DiaryId));
            used.UnionWith(watchlists.Where(w => !string.IsNullOrWhiteSpace(w.WatchlistId)).Select(w => w.WatchlistId));
            used.UnionWith(ratings.Where(r => !string.IsNullOrWhiteSpace(r.RatingId)).Select(r => r.RatingId!).Where(id => id != null));

            // Helper to decide if we should replace an id: we choose to replace if null/empty or contains '-' (GUID style) or length != desired
            bool NeedsReplace(string s) => string.IsNullOrWhiteSpace(s) || s.Contains("-") || s.Length != length;

            var favMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var diaryMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var watchMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var ratingMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            int favChanged = 0, diaryChanged = 0, watchlistChanged = 0, ratingChanged = 0;
            var samples = new List<(string oldId, string newId)>();

            // Prepare mappings
            string MakeUnique()
            {
                for (int attempts = 0; attempts < 10000; attempts++)
                {
                    var c = ShortId(rnd, length);
                    if (!used.Contains(c)) { used.Add(c); return c; }
                }
                throw new InvalidOperationException("Unable to generate unique short id");
            }

            foreach (var f in favs)
            {
                if (f.FavId != null && NeedsReplace(f.FavId))
                {
                    var newId = MakeUnique();
                    favMap[f.FavId ?? string.Empty] = newId;
                    favChanged++;
                    if (samples.Count < 10) samples.Add((f.FavId ?? "", newId));
                }
            }
            foreach (var d in diaries)
            {
                if (d.DiaryId != null && NeedsReplace(d.DiaryId))
                {
                    var newId = MakeUnique();
                    diaryMap[d.DiaryId ?? string.Empty] = newId;
                    diaryChanged++;
                    if (samples.Count < 10) samples.Add((d.DiaryId ?? "", newId));
                }
            }
            foreach (var w in watchlists)
            {
                if (w.WatchlistId != null && NeedsReplace(w.WatchlistId))
                {
                    var newId = MakeUnique();
                    watchMap[w.WatchlistId ?? string.Empty] = newId;
                    watchlistChanged++;
                    if (samples.Count < 10) samples.Add((w.WatchlistId ?? "", newId));
                }
            }
            foreach (var r in ratings)
            {
                if (r.RatingId != null && NeedsReplace(r.RatingId))
                {
                    var newId = MakeUnique();
                    ratingMap[r.RatingId ?? string.Empty] = newId;
                    ratingChanged++;
                    if (samples.Count < 10) samples.Add((r.RatingId ?? "", newId));
                }
            }

            if (!apply)
            {
                return (favChanged, diaryChanged, watchlistChanged, ratingChanged, samples);
            }

            // Apply changes inside DB transaction
            using var tx = this.Database.BeginTransaction();
            try
            {
                // Update Fav table
                var favEntities = this.Fav.ToList();
                foreach (var f in favEntities)
                {
                    if (favMap.TryGetValue(f.FavId ?? string.Empty, out var newId))
                    {
                        f.FavId = newId;
                        this.Fav.Update(f);
                    }
                }

                // Update Ratings
                var ratingEntities = this.Ratings.ToList();
                foreach (var r in ratingEntities)
                {
                    if (ratingMap.TryGetValue(r.RatingId ?? string.Empty, out var newId))
                    {
                        r.RatingId = newId;
                        this.Ratings.Update(r);
                    }
                }

                // Update Diary
                var diaryEntities = this.Diary.ToList();
                foreach (var d in diaryEntities)
                {
                    if (diaryMap.TryGetValue(d.DiaryId ?? string.Empty, out var newId))
                    {
                        d.DiaryId = newId;
                        this.Diary.Update(d);
                    }
                    // diary references ratingId - update if needed
                    if (!string.IsNullOrWhiteSpace(d.RatingId) && ratingMap.TryGetValue(d.RatingId, out var newRating))
                    {
                        d.RatingId = newRating;
                        this.Diary.Update(d);
                    }
                }

                // Update Watchlist
                var watchEntities = this.Watchlist.ToList();
                foreach (var w in watchEntities)
                {
                    if (watchMap.TryGetValue(w.WatchlistId ?? string.Empty, out var newId))
                    {
                        w.WatchlistId = newId;
                        this.Watchlist.Update(w);
                    }
                }

                // Update Users referencing these ids
                var userEntities = this.Users.ToList();
                foreach (var u in userEntities)
                {
                    bool changed = false;
                    if (!string.IsNullOrWhiteSpace(u.FavId) && favMap.TryGetValue(u.FavId, out var nf)) { u.FavId = nf; changed = true; }
                    if (!string.IsNullOrWhiteSpace(u.DiaryId) && diaryMap.TryGetValue(u.DiaryId, out var nd)) { u.DiaryId = nd; changed = true; }
                    if (!string.IsNullOrWhiteSpace(u.WatchlistId) && watchMap.TryGetValue(u.WatchlistId, out var nw)) { u.WatchlistId = nw; changed = true; }
                    if (changed) this.Users.Update(u);
                }

                // Update Watched entries referencing Diary or Fav
                var watchedEntities = this.Watched.ToList();
                foreach (var w in watchedEntities)
                {
                    bool changed = false;
                    if (!string.IsNullOrWhiteSpace(w.DiaryId) && diaryMap.TryGetValue(w.DiaryId, out var nd)) { w.DiaryId = nd; changed = true; }
                    if (!string.IsNullOrWhiteSpace(w.FavId) && favMap.TryGetValue(w.FavId, out var nf2)) { w.FavId = nf2; changed = true; }
                    if (changed) this.Watched.Update(w);
                }

                // Update Diary.RatingId already handled above

                this.SaveChanges();
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Normalization failed: {ex.Message}");
                tx.Rollback();
                throw;
            }

            // Return counts and sample pairs
            return (favChanged, diaryChanged, watchlistChanged, ratingChanged, samples);
        }

        // Design-time factory so `dotnet ef` can create the context even when DI is used.
        public class LetterBoxDbContextFactory : IDesignTimeDbContextFactory<LetterBoxDbContext>
        {
            public LetterBoxDbContext CreateDbContext(string[] args)
            {
                var dbName = Environment.GetEnvironmentVariable("LETTERBOX_DB") ?? "letterbox";
                var optionsBuilder = new DbContextOptionsBuilder<LetterBoxDbContext>();
                optionsBuilder.UseNpgsql($"Host=localhost;Database={dbName}");
                return new LetterBoxDbContext(optionsBuilder.Options);
            }
        }
    }
}
