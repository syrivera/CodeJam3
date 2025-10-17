using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeJam3b.Models.Users;
using CodeJam3b.Models.Lists;

namespace CodeJam3b.Models.Movies
{
    public class LetterBoxDbContext : DbContext
    {
        private readonly string _connectionHost = "localhost";
        private readonly string _connectionDbName;

        public LetterBoxDbContext(string dbName = "letterbox")
        {
            _connectionDbName = dbName;
        }

        public LetterBoxDbContext(DbContextOptions<LetterBoxDbContext> options) : base(options)
        {
            _connectionDbName = "letterbox";
        }

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
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql($"Host={_connectionHost};Database={_connectionDbName}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Minimal fix: explicitly configure one-to-one relationship between User and Watched
            modelBuilder.Entity<User>()
                .HasOne(u => u.Watched)
                .WithOne()
                .HasForeignKey<Watched>(w => w.UserId);
            base.OnModelCreating(modelBuilder);

            // Table mapping (optional, adjust if your tables are named differently)
            modelBuilder.Entity<Movie>().ToTable("movies");
            modelBuilder.Entity<Rating>().ToTable("ratings");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Watchlist>().ToTable("watchlist");
            modelBuilder.Entity<Watched>().ToTable("watched");
            modelBuilder.Entity<Fav>().ToTable("fav");
            modelBuilder.Entity<Diary>().ToTable("diary");

            // Keys (EF will usually infer these but we make them explicit)
            // (Assumes your model types declare their own [Key] or property named Id / rating_id etc.)
            modelBuilder.Entity<Movie>().HasKey("Id");
            modelBuilder.Entity<Rating>().HasKey("RatingId");
            modelBuilder.Entity<User>().HasKey("UserId");
            modelBuilder.Entity<Watchlist>().HasKey("WatchlistId");
            modelBuilder.Entity<Watched>().HasKey("Id");
            modelBuilder.Entity<Fav>().HasKey("FavId");
            modelBuilder.Entity<Diary>().HasKey("DiaryId");

            //
            // Generate seeds
            //
            var movieObjs = GenerateRealMovieObjects(250).ToArray();
            var userObjs = GenerateUserObjects(15).ToArray();

            // create containers for other tables
            var ratingObjs = new List<object>();
            var diaryObjs = new List<object>();
            var watchlistObjs = new List<object>();
            var watchedObjs = new List<object>();
            var favObjs = new List<object>();

            int ridCounter = 1, didCounter = 1, widCounter = 1, zidCounter = 1, fidCounter = 1;

            // For deterministic linking, choose movie indices using arithmetic spreads
            for (int ui = 0; ui < userObjs.Length; ui++)
            {
                var user = userObjs[ui];
                string uid = FormatId("UID", ui + 1, 3);

                // 3 ratings per user
                for (int r = 0; r < 3; r++)
                {
                    int mIndex = (ui * 7 + r * 11) % movieObjs.Length;
                    var mv = movieObjs[mIndex];
                    string rid = FormatId("RID", ridCounter++, 3);

                    var rating = CreateInstanceAndSet(typeof(Rating), new Dictionary<string, object?>
                    {
                        {"rating_id", rid}, {"RatingId", rid},
                        {"user_id", uid}, {"UserId", uid},
                        {"movie_id", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")},
                        {"movieId", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")},
                        {"score", CalculateScore(ui+1, r)}
                    });
                    ratingObjs.Add(rating);

                    // Create diary for every 2nd rating (deterministic)
                    if (r % 2 == 0)
                    {
                        string did = FormatId("DID", didCounter++, 3);
                        var diary = CreateInstanceAndSet(typeof(Diary), new Dictionary<string, object?>
                        {
                            {"diary_id", did}, {"DiaryId", did},
                            {"user_id", uid}, {"UserId", uid},
                            {"movie_id", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")},
                            {"watched_date", new DateTime(2023, ((ui % 9) + 1), ((r % 27) + 1))},
                        //    {"review", GenerateReviewText(uid, GetPropValueSafe(mv, "name") ?? GetPropValueSafe(mv, "Name")?.ToString() ?? "")},
                            {"rating_id", rid}, {"RatingId", rid}
                        });
                        diaryObjs.Add(diary);
                    }
                }

                // Watchlist: 3 unique movies
                for (int w = 0; w < 3; w++)
                {
                    int mIndex = (ui * 5 + w * 13) % movieObjs.Length;
                    var mv = movieObjs[mIndex];
                    string wid = FormatId("WID", widCounter++, 3);
                    var wl = CreateInstanceAndSet(typeof(Watchlist), new Dictionary<string, object?>
                    {
                        {"watchlist_id", wid}, {"WatchlistId", wid},
                        {"user_id", uid}, {"UserId", uid},
                        {"movie_id", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")}
                    });
                    watchlistObjs.Add(wl);
                }

                // Watched: 6 entries
                for (int z = 0; z < 6; z++)
                {
                    int mIndex = (ui * 9 + z * 17) % movieObjs.Length;
                    var mv = movieObjs[mIndex];
                    string zid = FormatId("ZID", zidCounter++, 4); // 4 digits
                    var watchedItem = CreateInstanceAndSet(typeof(Watched), new Dictionary<string, object?>
                    {
                        {"id", zid}, {"Id", zid},
                        {"user_id", uid}, {"UserId", uid},
                        {"movie_id", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")},
                        {"watched_date", new DateTime(2022, ((z % 12) + 1), ((ui + z) % 28 + 1))}
                    });
                    watchedObjs.Add(watchedItem);
                }

                // Favs: 2 per user
                for (int f = 0; f < 2; f++)
                {
                    int mIndex = (ui * 3 + f * 23) % movieObjs.Length;
                    var mv = movieObjs[mIndex];
                    string fid = FormatId("FID", fidCounter++, 3);
                    var favItem = CreateInstanceAndSet(typeof(Fav), new Dictionary<string, object?>
                    {
                        {"fav_id", fid}, {"FavId", fid},
                        {"user_id", uid}, {"UserId", uid},
                        {"movie_id", GetPropValueSafe(mv, "id") ?? GetPropValueSafe(mv, "Id")}
                    });
                    favObjs.Add(favItem);
                }
            }

            // Add seeds to ModelBuilder - note: HasData expects objects of the entity runtime type
            modelBuilder.Entity<Movie>().HasData(movieObjs);
            modelBuilder.Entity<User>().HasData(userObjs);
            modelBuilder.Entity<Rating>().HasData(ratingObjs.ToArray());
            modelBuilder.Entity<Diary>().HasData(diaryObjs.ToArray());
            modelBuilder.Entity<Watchlist>().HasData(watchlistObjs.ToArray());
            modelBuilder.Entity<Watched>().HasData(watchedObjs.ToArray());
            modelBuilder.Entity<Fav>().HasData(favObjs.ToArray());
        }

        // ------------------------
        // Helper: create seeded entity instance of given runtime type and set properties (tries both snake_case and PascalCase)
        // ------------------------
        private static object CreateInstanceAndSet(Type runtimeType, Dictionary<string, object?> props)
        {
            var inst = Activator.CreateInstance(runtimeType) ?? throw new InvalidOperationException($"Unable to instantiate {runtimeType}");
            foreach (var kv in props)
            {
                SetPropertyIfExists(inst, kv.Key, kv.Value);
            }
            return inst;
        }

        // Try to get a property value from object (snake_case or PascalCase)
        private static object? GetPropValueSafe(object obj, string name)
        {
            var t = obj.GetType();
            var p = t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return p?.GetValue(obj);
        }

        private static void SetPropertyIfExists(object obj, string propName, object? value)
        {
            var t = obj.GetType();
            // Try exact propName first, then ignore-case search
            var p = t.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (p == null)
            {
                // fallback: try ignoring case and common snake vs Pascal conversions
                p = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .FirstOrDefault(pi => string.Equals(pi.Name, propName, StringComparison.OrdinalIgnoreCase)
                                            || string.Equals(pi.Name.Replace("_", ""), propName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
            }
            if (p == null) return;

            if (value == null)
            {
                // If property type is non-nullable value type, skip setting null
                if (p.PropertyType.IsValueType && Nullable.GetUnderlyingType(p.PropertyType) == null) return;
                p.SetValue(obj, null);
                return;
            }

            try
            {
                var targetType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                var safeVal = Convert.ChangeType(value, targetType);
                p.SetValue(obj, safeVal);
            }
            catch
            {
                // best-effort: if conversion fails, attempt some common conversions
                try
                {
                    if (p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?))
                    {
                        if (Guid.TryParse(value.ToString(), out var g)) p.SetValue(obj, g);
                    }
                    // otherwise skip
                }
                catch { /* ignore and continue */ }
            }
        }

        // ------------------------
        // Movie seeding: real curated list (trimmed / extended as needed)
        // ------------------------
        private static IEnumerable<Movie> GenerateRealMovieObjects(int requiredCount)
        {
            // Multi-line curated list of real movies (title|year|genre|duration|avgRating)
            var data = @"
The Godfather|1972|drama|175|4.9
Citizen Kane|1941|drama|119|4.8
Pulp Fiction|1994|crime|154|4.7
Schindler's List|1993|drama|195|4.9
Casablanca|1942|romance|102|4.8
The Shawshank Redemption|1994|drama|142|4.9
The Dark Knight|2008|action|152|4.8
Inception|2010|sci-fi|148|4.7
2001: A Space Odyssey|1968|sci-fi|149|4.7
Star Wars: Episode IV - A New Hope|1977|sci-fi|121|4.6
The Empire Strikes Back|1980|sci-fi|124|4.8
Raiders of the Lost Ark|1981|adventure|115|4.6
Jaws|1975|thriller|124|4.4
Forrest Gump|1994|drama|142|4.6
Fight Club|1999|drama|139|4.6
The Matrix|1999|sci-fi|136|4.6
Goodfellas|1990|crime|146|4.7
One Flew Over the Cuckoo's Nest|1975|drama|133|4.7
The Silence of the Lambs|1991|thriller|118|4.6
Se7en|1995|thriller|127|4.6
Parasite|2019|thriller|132|4.6
Spirited Away|2001|animation|125|4.6
Your Name|2016|animation|106|4.5
The Lord of the Rings: The Fellowship of the Ring|2001|fantasy|178|4.7
The Lord of the Rings: The Two Towers|2002|fantasy|179|4.7
The Lord of the Rings: The Return of the King|2003|fantasy|201|4.8
La La Land|2016|musical|128|4.0
Moonlight|2016|drama|111|4.1
The Grand Budapest Hotel|2014|comedy|99|4.1
Mad Max: Fury Road|2015|action|120|4.1
The Revenant|2015|drama|156|4.0
Whiplash|2014|drama|106|4.2
Her|2013|romance|126|4.0
Eternal Sunshine of the Spotless Mind|2004|romance|108|4.3
Amélie|2001|romance|122|4.2
Pan's Labyrinth|2006|fantasy|118|4.2
Oldboy|2003|thriller|120|4.1
City of God|2002|crime|130|4.3
Crouching Tiger, Hidden Dragon|2000|action|120|4.1
The Bicycle Thieves|1948|drama|89|4.1
Rashomon|1950|mystery|88|4.0
Seven Samurai|1954|action|207|4.5
Tokyo Story|1953|drama|136|4.1
The Pianist|2002|drama|150|4.4
The Intouchables|2011|comedy|112|4.3
Gran Torino|2008|drama|116|4.1
No Country for Old Men|2007|thriller|122|4.2
There Will Be Blood|2007|drama|158|4.1
A Separation|2011|drama|123|4.3
The Lives of Others|2006|drama|137|4.3
The Shining|1980|horror|146|4.2
Blade Runner|1982|sci-fi|117|4.1
Blade Runner 2049|2017|sci-fi|164|4.0
The Truman Show|1998|drama|103|4.1
The Big Lebowski|1998|comedy|117|4.1
Toy Story|1995|animation|81|4.1
Toy Story 3|2010|animation|103|4.2
Inside Out|2015|animation|95|4.2
WALL·E|2008|animation|98|4.1
Up|2009|animation|96|4.1
The Incredibles|2004|animation|115|4.0
Finding Nemo|2003|animation|100|4.0
The Lion King|1994|animation|88|4.1
Back to the Future|1985|adventure|116|4.3
The Terminator|1984|sci-fi|107|4.0
Terminator 2: Judgment Day|1991|action|137|4.3
Alien|1979|horror|116|4.1
Aliens|1986|action|137|4.0
The Departed|2006|crime|151|4.2
Memento|2000|mystery|113|4.2
The Prestige|2006|drama|130|4.1
Inglourious Basterds|2009|war|153|4.1
Django Unchained|2012|western|165|4.1
Gladiator|2000|action|155|4.2
Titanic|1997|romance|195|4.1
The Wolf of Wall Street|2013|biography|180|4.0
Shutter Island|2010|mystery|138|4.0
Taxi Driver|1976|crime|114|4.2
Good Will Hunting|1997|drama|126|4.1
The Exorcist|1973|horror|122|4.0
A Clockwork Orange|1971|crime|136|4.0
The Third Man|1949|mystery|104|4.1
Raging Bull|1980|drama|129|4.1
The Graduate|1967|comedy|106|4.0
Breakfast at Tiffany's|1961|romance|115|4.0
Psycho|1960|horror|109|4.1
Vertigo|1958|mystery|128|4.1
North by Northwest|1959|thriller|136|4.1
My Neighbor Totoro|1988|animation|86|4.1
The Seventh Seal|1957|drama|96|4.0
Persona|1966|drama|84|4.0
Mulholland Drive|2001|mystery|147|4.0
The Thing|1982|horror|109|4.0
The Great Dictator|1940|comedy|125|4.1
The General|1926|comedy|67|4.0
Metropolis|1927|sci-fi|153|4.0
Sunrise: A Song of Two Humans|1927|drama|95|4.0
Battleship Potemkin|1925|drama|75|4.0
Breathless|1960|crime|90|4.0
Jules and Jim|1962|romance|105|4.0
La Dolce Vita|1960|drama|174|4.0
8½|1963|drama|138|4.0
The 400 Blows|1959|drama|99|4.0
Aguirre, the Wrath of God|1972|adventure|93|4.0
Fitzcarraldo|1982|adventure|158|4.0
Stalker|1979|sci-fi|163|4.0
Andrei Rublev|1966|drama|205|4.0
Dances with Wolves|1990|drama|181|4.0
The Last Emperor|1987|drama|163|4.0
Capernaum|2018|drama|126|4.1
Portrait of a Lady on Fire|2019|romance|122|4.1
The Handmaiden|2016|thriller|145|4.1
Talk to Her|2002|drama|112|4.0
All About My Mother|1999|drama|101|4.0
Volver|2006|drama|121|4.0
Downfall|2004|drama|156|4.0
The Hunt|2012|drama|115|4.0
The Square|2017|drama|142|3.9
The Artist|2011|comedy|100|3.8
Boyhood|2014|drama|165|4.0
The Favourite|2018|drama|119|4.0
Three Billboards Outside Ebbing, Missouri|2017|drama|115|4.0
Manchester by the Sea|2016|drama|137|4.0
Room|2015|drama|118|4.0
Spotlight|2015|drama|129|4.0
Argo|2012|thriller|120|4.0
Zero Dark Thirty|2012|thriller|157|3.9
Black Swan|2010|drama|108|4.0
Little Women|2019|drama|135|3.9
Lady Bird|2017|drama|94|3.9
Frances Ha|2012|comedy|86|3.8
Moon|2009|sci-fi|97|4.0
Ex Machina|2014|sci-fi|108|4.0
Drive|2011|crime|100|4.0
Blade Runner: The Final Cut|1982|sci-fi|117|4.1
Sicario|2015|thriller|121|3.9
Prisoners|2013|thriller|153|4.0
The Big Short|2015|drama|130|3.8
The King's Speech|2010|drama|119|4.0
Hidden Figures|2016|drama|127|3.8
Dallas Buyers Club|2013|drama|117|4.0
The Hurt Locker|2008|war|131|3.9
The Tree of Life|2011|drama|139|3.7
Birdman or (The Unexpected Virtue of Ignorance)|2014|drama|119|3.9
In the Mood for Love|2000|romance|98|4.1
Chungking Express|1994|romance|102|3.9
Farewell My Concubine|1993|drama|171|4.0
Raise the Red Lantern|1991|drama|125|3.9
A Prophet|2009|crime|154|3.9
Incendies|2010|drama|130|4.0
Wings of Desire|1987|drama|128|4.0
The Maltese Falcon|1941|mystery|100|4.0
Scarface|1983|crime|170|4.0
Heat|1995|crime|170|4.0
L.A. Confidential|1997|crime|138|4.0
The French Connection|1971|crime|104|4.0
The Last Picture Show|1971|drama|119|4.0
Network|1976|drama|121|4.0
When Harry Met Sally...|1989|romance|96|3.9
Annie Hall|1977|comedy|93|4.0
To Kill a Mockingbird|1962|drama|129|4.1
Ratatouille|2007|animation|111|4.0
Kubo and the Two Strings|2016|animation|101|4.0
Singin' in the Rain|1952|musical|103|4.1
It Happened One Night|1934|romance|105|4.0
Modern Times|1936|comedy|87|4.0
Some Like It Hot|1959|comedy|121|4.1
Do the Right Thing|1989|drama|120|4.0
Selma|2014|drama|128|3.9
Brokeback Mountain|2005|romance|134|3.9
The Piano|1993|drama|111|4.0
The Remains of the Day|1993|drama|134|4.0
Adaptation.|2002|comedy|114|3.9
Match Point|2005|thriller|124|3.8
The Thin Red Line|1998|war|170|3.9
Paths of Glory|1957|war|88|4.0
The Wages of Fear|1953|thriller|138|3.9
The Best Years of Our Lives|1946|drama|172|3.9
The Philadelphia Story|1940|comedy|112|3.9
The Passion of Joan of Arc|1928|drama|96|4.0
Wuthering Heights|1939|romance|103|3.8
Rebecca|1940|mystery|130|3.9
The Conversation|1974|thriller|113|3.9
The Magnificent Ambersons|1942|drama|88|3.7
L'Avventura|1960|mystery|144|3.8
My Fair Lady|1964|musical|170|3.9
Oliver!|1968|musical|153|3.8
Cabaret|1972|musical|124|3.9
Sound of Metal|2019|drama|120|3.8
Nomadland|2020|drama|108|3.8
Promising Young Woman|2020|thriller|113|3.8
Minari|2020|drama|115|3.8
The Trial of the Chicago 7|2020|drama|130|3.8
Soul|2020|animation|100|4.0
Tenet|2020|sci-fi|150|3.7
Dune|2021|sci-fi|155|3.9
The Power of the Dog|2021|drama|126|3.9
CODA|2021|drama|111|3.9
West Side Story|2021|musical|156|3.8
Drive My Car|2021|drama|179|3.9
Decision to Leave|2022|romance|139|3.9
Triangle of Sadness|2022|satire|147|3.6
All Quiet on the Western Front (2022)|2022|war|147|3.9
Barbie|2023|comedy|114|3.7
Oppenheimer|2023|drama|180|4.2
Past Lives|2023|romance|105|4.0
Spider-Man: Across the Spider-Verse|2023|animation|140|4.2
The Zone of Interest|2023|drama|130|3.7
Anatomy of a Fall|2023|mystery|165|3.8
Poor Things|2023|fantasy|150|3.8
Next Goal Wins|2023|comedy|108|3.6
Saltburn|2023|drama|140|3.5
The Holdovers|2023|drama|130|3.8
Killers of the Flower Moon|2023|crime|206|3.8
May December|2023|drama|140|3.6
American Fiction|2023|drama|128|3.7
The Color Purple (2023)|2023|drama|135|3.6
The Creator|2023|sci-fi|112|3.4
The Iron Claw|2023|drama|137|3.6
Are You There God? It's Me, Margaret.|2023|drama|120|3.5
";

            var lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(l => l.Trim())
                            .Where(l => l.Length > 0)
                            .ToArray();

            var movies = new List<Movie>(requiredCount);
            int idx = 1;
            foreach (var line in lines)
            {
                if (movies.Count >= requiredCount) break;
                var parts = line.Split('|');
                if (parts.Length < 5) continue;
                string title = parts[0].Trim();
                int year = int.TryParse(parts[1].Trim(), out var y) ? y : 2000;
                string genre = parts[2].Trim();
                int duration = int.TryParse(parts[3].Trim(), out var d) ? d : 100;
                double avgRating = double.TryParse(parts[4].Trim(), out var a) ? a : 3.5;

                // Create instance of Movie type
                var movieObj = Activator.CreateInstance(typeof(Movie)) ?? throw new InvalidOperationException("Movie type not found at runtime.");

                // Set common property names (try both snake_case and PascalCase)
                SetPropertyIfExists(movieObj, "id", FormatId("MID", idx, 3));
                SetPropertyIfExists(movieObj, "Id", FormatId("MID", idx, 3));
                SetPropertyIfExists(movieObj, "name", title);
                SetPropertyIfExists(movieObj, "Name", title);
                SetPropertyIfExists(movieObj, "release_year", year);
                SetPropertyIfExists(movieObj, "ReleaseYear", year);
                SetPropertyIfExists(movieObj, "genre", genre);
                SetPropertyIfExists(movieObj, "Genre", genre);
                SetPropertyIfExists(movieObj, "duration_mins", duration);
                SetPropertyIfExists(movieObj, "DurationMins", duration);
                SetPropertyIfExists(movieObj, "avg_rating", avgRating);
                SetPropertyIfExists(movieObj, "AvgRating", avgRating);
                // rating_id may remain null for initial seed
                SetPropertyIfExists(movieObj, "rating_id", null);
                SetPropertyIfExists(movieObj, "RatingId", null);

                movies.Add((Movie)movieObj);
                idx++;
            }

            // Fill with placeholders until requiredCount
            while (movies.Count < requiredCount)
            {
                int i = movies.Count + 1;
                var movieObj = Activator.CreateInstance(typeof(Movie)) ?? throw new InvalidOperationException("Movie type not found at runtime.");
                SetPropertyIfExists(movieObj, "id", FormatId("MID", i, 3));
                SetPropertyIfExists(movieObj, "Id", FormatId("MID", i, 3));
                SetPropertyIfExists(movieObj, "name", $"Untitled Film {i:D3}");
                SetPropertyIfExists(movieObj, "Name", $"Untitled Film {i:D3}");
                SetPropertyIfExists(movieObj, "release_year", 2000);
                SetPropertyIfExists(movieObj, "ReleaseYear", 2000);
                SetPropertyIfExists(movieObj, "genre", "drama");
                SetPropertyIfExists(movieObj, "Genre", "drama");
                SetPropertyIfExists(movieObj, "duration_mins", 100);
                SetPropertyIfExists(movieObj, "DurationMins", 100);
                SetPropertyIfExists(movieObj, "avg_rating", 3.5);
                SetPropertyIfExists(movieObj, "AvgRating", 3.5);

                movies.Add((Movie)movieObj);
            }

            return movies;
        }

        // ------------------------
        // Users seeding (15 realistic Letterboxd-style users)
        // ------------------------
        private static IEnumerable<User> GenerateUserObjects(int count)
        {
            var handles = new[]
            {
                "sam", "jordan", "kristian", "nocturne_curious", "eighties_heart",
                "docu_hoarder", "romcom_ronin", "action_alex", "wanderlust_wren",
                "spicy_subtitles", "classic_cleo", "pixel_popcorn", "quiet_projection",
                "film_basement", "cassette_cinema"
            };

            var bios = new[]
            {
                "Film buff, coffee addict, aspiring screenwriter. Always searching for the next mind-bending thriller.",
                "Loves indie films, hiking, and spontaneous road trips. Can quote Tarantino on a Tuesday.",
                "Animation enthusiast, board game champion, and collector of obscure movie trivia.",
                "Late-night movie watcher. Believes popcorn is a food group and that 2 a.m. is peak cinema time.",
                "Cinephile with a soft spot for 80s soundtracks and epic fantasy sagas.",
                "Documentary lover, amateur photographer, festival regular—asks too many questions during Q&As.",
                "Always up for a rom-com marathon. Writes reviews using too many heart emojis.",
                "Action junkie, sci-fi dreamer, self-proclaimed movie quote master—owns a themed jacket.",
                "Traveler and cinephile. Watches foreign films on trains and rates snacks by region.",
                "Enjoys foreign films, spicy food, and debating plot twists at midnight.",
                "Classic cinema devotee. Can recite Casablanca, but only the good lines.",
                "Indie filmmaker who takes stills of the credits and frames them at home.",
                "Quiet critic: short, sharp reviews. Prefers rainy-day cinema.",
                "Basement projector owner. Hosts monthly midnight screenings with suspiciously good nachos.",
                "Collector of mixtape soundtracks and late-run double features."
            };

            var users = new List<User>(count);
            for (int i = 1; i <= count; i++)
            {
                string uid = FormatId("UID", i, 3);
                string handle = handles[(i - 1) % handles.Length] + ((i > handles.Length) ? (i - handles.Length).ToString() : "");
                string email = $"{handle}{i}@letterbox.test";
                string bio = bios[(i - 1) % bios.Length];

                var userObj = Activator.CreateInstance(typeof(User)) ?? throw new InvalidOperationException("User type not found at runtime.");
                SetPropertyIfExists(userObj, "user_id", uid);
                SetPropertyIfExists(userObj, "UserId", uid);
                SetPropertyIfExists(userObj, "username", handle);
                SetPropertyIfExists(userObj, "Username", handle);
                SetPropertyIfExists(userObj, "email", email);
                SetPropertyIfExists(userObj, "Email", email);
                SetPropertyIfExists(userObj, "bio", bio);
                SetPropertyIfExists(userObj, "Bio", bio);
                // optional helper id fields left null
                SetPropertyIfExists(userObj, "watched_id", null);
                SetPropertyIfExists(userObj, "WatchedId", null);
                SetPropertyIfExists(userObj, "liked_id", null);
                SetPropertyIfExists(userObj, "LikedId", null);
                SetPropertyIfExists(userObj, "watchlist_id", null);
                SetPropertyIfExists(userObj, "WatchlistId", null);
                SetPropertyIfExists(userObj, "diary_id", null);
                SetPropertyIfExists(userObj, "DiaryId", null);

                users.Add((User)userObj);
            }

            return users;
        }

        // ------------------------
        // Small deterministic helpers
        // ------------------------
        private static string FormatId(string prefix, int number, int width = 3) =>
            $"{prefix}{number.ToString($"D{width}")}";

        private static double CalculateScore(int userIndex, int innerIndex)
        {
            double baseScore = 2.5 + ((userIndex % 5) * 0.4);
            double extra = (innerIndex % 3) * 0.6;
            return Math.Round(Math.Min(5.0, baseScore + extra), 1);
        }

        private static string GenerateReviewText(string uid, string movieName)
        {
            return uid switch
            {
                string u when u.EndsWith("001") => $"An intimate, unsettling watch — {movieName} lodged in my head for days.",
                string u when u.EndsWith("002") => $"Sharp pacing and a killer score. {movieName} kept me guessing.",
                string u when u.EndsWith("003") => $"Lovely visuals, uneven script — still worth a watch for the performances.",
                string u when u.EndsWith("004") => $"Comfort food cinema. {movieName} felt like warm rain on a slow evening.",
                string u when u.EndsWith("005") => $"Soundtrack slaps. A small gem that refuses to leave.",
                string u when u.EndsWith("006") => $"Impressively staged and quietly devastating. Great craft throughout.",
                string u when u.EndsWith("007") => $"Cute, funny, and oddly sincere. Left with a grin.",
                string u when u.EndsWith("008") => $"Full-throttle action with heart. Stunts are insane.",
                string u when u.EndsWith("009") => $"A travelogue of feelings. I want to rewatch on a rainy morning.",
                string u when u.EndsWith("010") => $"Subtitles and spice — one of my favorites this year.",
                string u when u.EndsWith("011") => $"So classic it aches. An homage done right.",
                string u when u.EndsWith("012") => $"Indie energy and a handful of scenes that land perfectly.",
                string u when u.EndsWith("013") => $"Understated, thoughtful, and somehow very brave. Loved it.",
                string u when u.EndsWith("014") => $"Midnight vibes and messy characters. My kind of film.",
                _ => $"An interesting watch — {movieName} has moments of real brilliance."
            };
        }

        // Design-time factory
        public class LetterBoxDbContextFactory : IDesignTimeDbContextFactory<LetterBoxDbContext>
        {
            public LetterBoxDbContext CreateDbContext(string[] args)
            {
                var dbName = Environment.GetEnvironmentVariable("LETTERBOX_DB") ?? "letterbox";
                var optionsBuilder = new DbContextOptionsBuilder<LetterBoxDbContext>();
                optionsBuilder.UseNpgsql($"Host=localhost;Database={dbName}");
                return new LetterBoxDbContext(dbName);
            }
        }
    }
}
