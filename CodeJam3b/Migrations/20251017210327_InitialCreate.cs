using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeJam3b.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diary",
                columns: table => new
                {
                    diary_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: true),
                    rating_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diary", x => x.diary_id);
                });

            migrationBuilder.CreateTable(
                name: "fav",
                columns: table => new
                {
                    fav_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fav", x => x.fav_id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    release_year = table.Column<int>(type: "integer", nullable: true),
                    genre = table.Column<string>(type: "text", nullable: true),
                    duration_mins = table.Column<int>(type: "integer", nullable: true),
                    avg_rating = table.Column<double>(type: "double precision", nullable: true),
                    rating_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "watched",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    fav_id = table.Column<string>(type: "text", nullable: true),
                    diary_id = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watched", x => x.id);
                    table.ForeignKey(
                        name: "FK_watched_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "watchlist",
                columns: table => new
                {
                    watchlist_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<string>(type: "text", nullable: true),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watchlist", x => x.watchlist_id);
                    table.ForeignKey(
                        name: "FK_watchlist_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    watched_id = table.Column<string>(type: "text", nullable: true),
                    list_id = table.Column<string>(type: "text", nullable: true),
                    watchlist_id = table.Column<string>(type: "text", nullable: true),
                    diary_id = table.Column<string>(type: "text", nullable: true),
                    WatchedId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_diary_diary_id",
                        column: x => x.diary_id,
                        principalTable: "diary",
                        principalColumn: "diary_id");
                    table.ForeignKey(
                        name: "FK_users_watched_WatchedId1",
                        column: x => x.WatchedId1,
                        principalTable: "watched",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_watchlist_watchlist_id",
                        column: x => x.watchlist_id,
                        principalTable: "watchlist",
                        principalColumn: "watchlist_id");
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    rating_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true),
                    movie_name = table.Column<string>(type: "text", nullable: true),
                    stars = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK_ratings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "id", "avg_rating", "duration_mins", "genre", "name", "rating_id", "release_year" },
                values: new object[,]
                {
                    { "MID001", 4.9000000000000004, 175, "drama", "The Godfather", null, 1972 },
                    { "MID002", 4.7999999999999998, 119, "drama", "Citizen Kane", null, 1941 },
                    { "MID003", 4.7000000000000002, 154, "crime", "Pulp Fiction", null, 1994 },
                    { "MID004", 4.9000000000000004, 195, "drama", "Schindler's List", null, 1993 },
                    { "MID005", 4.7999999999999998, 102, "romance", "Casablanca", null, 1942 },
                    { "MID006", 4.9000000000000004, 142, "drama", "The Shawshank Redemption", null, 1994 },
                    { "MID007", 4.7999999999999998, 152, "action", "The Dark Knight", null, 2008 },
                    { "MID008", 4.7000000000000002, 148, "sci-fi", "Inception", null, 2010 },
                    { "MID009", 4.7000000000000002, 149, "sci-fi", "2001: A Space Odyssey", null, 1968 },
                    { "MID010", 4.5999999999999996, 121, "sci-fi", "Star Wars: Episode IV - A New Hope", null, 1977 },
                    { "MID011", 4.7999999999999998, 124, "sci-fi", "The Empire Strikes Back", null, 1980 },
                    { "MID012", 4.5999999999999996, 115, "adventure", "Raiders of the Lost Ark", null, 1981 },
                    { "MID013", 4.4000000000000004, 124, "thriller", "Jaws", null, 1975 },
                    { "MID014", 4.5999999999999996, 142, "drama", "Forrest Gump", null, 1994 },
                    { "MID015", 4.5999999999999996, 139, "drama", "Fight Club", null, 1999 },
                    { "MID016", 4.5999999999999996, 136, "sci-fi", "The Matrix", null, 1999 },
                    { "MID017", 4.7000000000000002, 146, "crime", "Goodfellas", null, 1990 },
                    { "MID018", 4.7000000000000002, 133, "drama", "One Flew Over the Cuckoo's Nest", null, 1975 },
                    { "MID019", 4.5999999999999996, 118, "thriller", "The Silence of the Lambs", null, 1991 },
                    { "MID020", 4.5999999999999996, 127, "thriller", "Se7en", null, 1995 },
                    { "MID021", 4.5999999999999996, 132, "thriller", "Parasite", null, 2019 },
                    { "MID022", 4.5999999999999996, 125, "animation", "Spirited Away", null, 2001 },
                    { "MID023", 4.5, 106, "animation", "Your Name", null, 2016 },
                    { "MID024", 4.7000000000000002, 178, "fantasy", "The Lord of the Rings: The Fellowship of the Ring", null, 2001 },
                    { "MID025", 4.7000000000000002, 179, "fantasy", "The Lord of the Rings: The Two Towers", null, 2002 },
                    { "MID026", 4.7999999999999998, 201, "fantasy", "The Lord of the Rings: The Return of the King", null, 2003 },
                    { "MID027", 4.0, 128, "musical", "La La Land", null, 2016 },
                    { "MID028", 4.0999999999999996, 111, "drama", "Moonlight", null, 2016 },
                    { "MID029", 4.0999999999999996, 99, "comedy", "The Grand Budapest Hotel", null, 2014 },
                    { "MID030", 4.0999999999999996, 120, "action", "Mad Max: Fury Road", null, 2015 },
                    { "MID031", 4.0, 156, "drama", "The Revenant", null, 2015 },
                    { "MID032", 4.2000000000000002, 106, "drama", "Whiplash", null, 2014 },
                    { "MID033", 4.0, 126, "romance", "Her", null, 2013 },
                    { "MID034", 4.2999999999999998, 108, "romance", "Eternal Sunshine of the Spotless Mind", null, 2004 },
                    { "MID035", 4.2000000000000002, 122, "romance", "Amélie", null, 2001 },
                    { "MID036", 4.2000000000000002, 118, "fantasy", "Pan's Labyrinth", null, 2006 },
                    { "MID037", 4.0999999999999996, 120, "thriller", "Oldboy", null, 2003 },
                    { "MID038", 4.2999999999999998, 130, "crime", "City of God", null, 2002 },
                    { "MID039", 4.0999999999999996, 120, "action", "Crouching Tiger, Hidden Dragon", null, 2000 },
                    { "MID040", 4.0999999999999996, 89, "drama", "The Bicycle Thieves", null, 1948 },
                    { "MID041", 4.0, 88, "mystery", "Rashomon", null, 1950 },
                    { "MID042", 4.5, 207, "action", "Seven Samurai", null, 1954 },
                    { "MID043", 4.0999999999999996, 136, "drama", "Tokyo Story", null, 1953 },
                    { "MID044", 4.4000000000000004, 150, "drama", "The Pianist", null, 2002 },
                    { "MID045", 4.2999999999999998, 112, "comedy", "The Intouchables", null, 2011 },
                    { "MID046", 4.0999999999999996, 116, "drama", "Gran Torino", null, 2008 },
                    { "MID047", 4.2000000000000002, 122, "thriller", "No Country for Old Men", null, 2007 },
                    { "MID048", 4.0999999999999996, 158, "drama", "There Will Be Blood", null, 2007 },
                    { "MID049", 4.2999999999999998, 123, "drama", "A Separation", null, 2011 },
                    { "MID050", 4.2999999999999998, 137, "drama", "The Lives of Others", null, 2006 },
                    { "MID051", 4.2000000000000002, 146, "horror", "The Shining", null, 1980 },
                    { "MID052", 4.0999999999999996, 117, "sci-fi", "Blade Runner", null, 1982 },
                    { "MID053", 4.0, 164, "sci-fi", "Blade Runner 2049", null, 2017 },
                    { "MID054", 4.0999999999999996, 103, "drama", "The Truman Show", null, 1998 },
                    { "MID055", 4.0999999999999996, 117, "comedy", "The Big Lebowski", null, 1998 },
                    { "MID056", 4.0999999999999996, 81, "animation", "Toy Story", null, 1995 },
                    { "MID057", 4.2000000000000002, 103, "animation", "Toy Story 3", null, 2010 },
                    { "MID058", 4.2000000000000002, 95, "animation", "Inside Out", null, 2015 },
                    { "MID059", 4.0999999999999996, 98, "animation", "WALL·E", null, 2008 },
                    { "MID060", 4.0999999999999996, 96, "animation", "Up", null, 2009 },
                    { "MID061", 4.0, 115, "animation", "The Incredibles", null, 2004 },
                    { "MID062", 4.0, 100, "animation", "Finding Nemo", null, 2003 },
                    { "MID063", 4.0999999999999996, 88, "animation", "The Lion King", null, 1994 },
                    { "MID064", 4.2999999999999998, 116, "adventure", "Back to the Future", null, 1985 },
                    { "MID065", 4.0, 107, "sci-fi", "The Terminator", null, 1984 },
                    { "MID066", 4.2999999999999998, 137, "action", "Terminator 2: Judgment Day", null, 1991 },
                    { "MID067", 4.0999999999999996, 116, "horror", "Alien", null, 1979 },
                    { "MID068", 4.0, 137, "action", "Aliens", null, 1986 },
                    { "MID069", 4.2000000000000002, 151, "crime", "The Departed", null, 2006 },
                    { "MID070", 4.2000000000000002, 113, "mystery", "Memento", null, 2000 },
                    { "MID071", 4.0999999999999996, 130, "drama", "The Prestige", null, 2006 },
                    { "MID072", 4.0999999999999996, 153, "war", "Inglourious Basterds", null, 2009 },
                    { "MID073", 4.0999999999999996, 165, "western", "Django Unchained", null, 2012 },
                    { "MID074", 4.2000000000000002, 155, "action", "Gladiator", null, 2000 },
                    { "MID075", 4.0999999999999996, 195, "romance", "Titanic", null, 1997 },
                    { "MID076", 4.0, 180, "biography", "The Wolf of Wall Street", null, 2013 },
                    { "MID077", 4.0, 138, "mystery", "Shutter Island", null, 2010 },
                    { "MID078", 4.2000000000000002, 114, "crime", "Taxi Driver", null, 1976 },
                    { "MID079", 4.0999999999999996, 126, "drama", "Good Will Hunting", null, 1997 },
                    { "MID080", 4.0, 122, "horror", "The Exorcist", null, 1973 },
                    { "MID081", 4.0, 136, "crime", "A Clockwork Orange", null, 1971 },
                    { "MID082", 4.0999999999999996, 104, "mystery", "The Third Man", null, 1949 },
                    { "MID083", 4.0999999999999996, 129, "drama", "Raging Bull", null, 1980 },
                    { "MID084", 4.0, 106, "comedy", "The Graduate", null, 1967 },
                    { "MID085", 4.0, 115, "romance", "Breakfast at Tiffany's", null, 1961 },
                    { "MID086", 4.0999999999999996, 109, "horror", "Psycho", null, 1960 },
                    { "MID087", 4.0999999999999996, 128, "mystery", "Vertigo", null, 1958 },
                    { "MID088", 4.0999999999999996, 136, "thriller", "North by Northwest", null, 1959 },
                    { "MID089", 4.0999999999999996, 86, "animation", "My Neighbor Totoro", null, 1988 },
                    { "MID090", 4.0, 96, "drama", "The Seventh Seal", null, 1957 },
                    { "MID091", 4.0, 84, "drama", "Persona", null, 1966 },
                    { "MID092", 4.0, 147, "mystery", "Mulholland Drive", null, 2001 },
                    { "MID093", 4.0, 109, "horror", "The Thing", null, 1982 },
                    { "MID094", 4.0999999999999996, 125, "comedy", "The Great Dictator", null, 1940 },
                    { "MID095", 4.0, 67, "comedy", "The General", null, 1926 },
                    { "MID096", 4.0, 153, "sci-fi", "Metropolis", null, 1927 },
                    { "MID097", 4.0, 95, "drama", "Sunrise: A Song of Two Humans", null, 1927 },
                    { "MID098", 4.0, 75, "drama", "Battleship Potemkin", null, 1925 },
                    { "MID099", 4.0, 90, "crime", "Breathless", null, 1960 },
                    { "MID100", 4.0, 105, "romance", "Jules and Jim", null, 1962 },
                    { "MID101", 4.0, 174, "drama", "La Dolce Vita", null, 1960 },
                    { "MID102", 4.0, 138, "drama", "8½", null, 1963 },
                    { "MID103", 4.0, 99, "drama", "The 400 Blows", null, 1959 },
                    { "MID104", 4.0, 93, "adventure", "Aguirre, the Wrath of God", null, 1972 },
                    { "MID105", 4.0, 158, "adventure", "Fitzcarraldo", null, 1982 },
                    { "MID106", 4.0, 163, "sci-fi", "Stalker", null, 1979 },
                    { "MID107", 4.0, 205, "drama", "Andrei Rublev", null, 1966 },
                    { "MID108", 4.0, 181, "drama", "Dances with Wolves", null, 1990 },
                    { "MID109", 4.0, 163, "drama", "The Last Emperor", null, 1987 },
                    { "MID110", 4.0999999999999996, 126, "drama", "Capernaum", null, 2018 },
                    { "MID111", 4.0999999999999996, 122, "romance", "Portrait of a Lady on Fire", null, 2019 },
                    { "MID112", 4.0999999999999996, 145, "thriller", "The Handmaiden", null, 2016 },
                    { "MID113", 4.0, 112, "drama", "Talk to Her", null, 2002 },
                    { "MID114", 4.0, 101, "drama", "All About My Mother", null, 1999 },
                    { "MID115", 4.0, 121, "drama", "Volver", null, 2006 },
                    { "MID116", 4.0, 156, "drama", "Downfall", null, 2004 },
                    { "MID117", 4.0, 115, "drama", "The Hunt", null, 2012 },
                    { "MID118", 3.8999999999999999, 142, "drama", "The Square", null, 2017 },
                    { "MID119", 3.7999999999999998, 100, "comedy", "The Artist", null, 2011 },
                    { "MID120", 4.0, 165, "drama", "Boyhood", null, 2014 },
                    { "MID121", 4.0, 119, "drama", "The Favourite", null, 2018 },
                    { "MID122", 4.0, 115, "drama", "Three Billboards Outside Ebbing, Missouri", null, 2017 },
                    { "MID123", 4.0, 137, "drama", "Manchester by the Sea", null, 2016 },
                    { "MID124", 4.0, 118, "drama", "Room", null, 2015 },
                    { "MID125", 4.0, 129, "drama", "Spotlight", null, 2015 },
                    { "MID126", 4.0, 120, "thriller", "Argo", null, 2012 },
                    { "MID127", 3.8999999999999999, 157, "thriller", "Zero Dark Thirty", null, 2012 },
                    { "MID128", 4.0, 108, "drama", "Black Swan", null, 2010 },
                    { "MID129", 3.8999999999999999, 135, "drama", "Little Women", null, 2019 },
                    { "MID130", 3.8999999999999999, 94, "drama", "Lady Bird", null, 2017 },
                    { "MID131", 3.7999999999999998, 86, "comedy", "Frances Ha", null, 2012 },
                    { "MID132", 4.0, 97, "sci-fi", "Moon", null, 2009 },
                    { "MID133", 4.0, 108, "sci-fi", "Ex Machina", null, 2014 },
                    { "MID134", 4.0, 100, "crime", "Drive", null, 2011 },
                    { "MID135", 4.0999999999999996, 117, "sci-fi", "Blade Runner: The Final Cut", null, 1982 },
                    { "MID136", 3.8999999999999999, 121, "thriller", "Sicario", null, 2015 },
                    { "MID137", 4.0, 153, "thriller", "Prisoners", null, 2013 },
                    { "MID138", 3.7999999999999998, 130, "drama", "The Big Short", null, 2015 },
                    { "MID139", 4.0, 119, "drama", "The King's Speech", null, 2010 },
                    { "MID140", 3.7999999999999998, 127, "drama", "Hidden Figures", null, 2016 },
                    { "MID141", 4.0, 117, "drama", "Dallas Buyers Club", null, 2013 },
                    { "MID142", 3.8999999999999999, 131, "war", "The Hurt Locker", null, 2008 },
                    { "MID143", 3.7000000000000002, 139, "drama", "The Tree of Life", null, 2011 },
                    { "MID144", 3.8999999999999999, 119, "drama", "Birdman or (The Unexpected Virtue of Ignorance)", null, 2014 },
                    { "MID145", 4.0999999999999996, 98, "romance", "In the Mood for Love", null, 2000 },
                    { "MID146", 3.8999999999999999, 102, "romance", "Chungking Express", null, 1994 },
                    { "MID147", 4.0, 171, "drama", "Farewell My Concubine", null, 1993 },
                    { "MID148", 3.8999999999999999, 125, "drama", "Raise the Red Lantern", null, 1991 },
                    { "MID149", 3.8999999999999999, 154, "crime", "A Prophet", null, 2009 },
                    { "MID150", 4.0, 130, "drama", "Incendies", null, 2010 },
                    { "MID151", 4.0, 128, "drama", "Wings of Desire", null, 1987 },
                    { "MID152", 4.0, 100, "mystery", "The Maltese Falcon", null, 1941 },
                    { "MID153", 4.0, 170, "crime", "Scarface", null, 1983 },
                    { "MID154", 4.0, 170, "crime", "Heat", null, 1995 },
                    { "MID155", 4.0, 138, "crime", "L.A. Confidential", null, 1997 },
                    { "MID156", 4.0, 104, "crime", "The French Connection", null, 1971 },
                    { "MID157", 4.0, 119, "drama", "The Last Picture Show", null, 1971 },
                    { "MID158", 4.0, 121, "drama", "Network", null, 1976 },
                    { "MID159", 3.8999999999999999, 96, "romance", "When Harry Met Sally...", null, 1989 },
                    { "MID160", 4.0, 93, "comedy", "Annie Hall", null, 1977 },
                    { "MID161", 4.0999999999999996, 129, "drama", "To Kill a Mockingbird", null, 1962 },
                    { "MID162", 4.0, 111, "animation", "Ratatouille", null, 2007 },
                    { "MID163", 4.0, 101, "animation", "Kubo and the Two Strings", null, 2016 },
                    { "MID164", 4.0999999999999996, 103, "musical", "Singin' in the Rain", null, 1952 },
                    { "MID165", 4.0, 105, "romance", "It Happened One Night", null, 1934 },
                    { "MID166", 4.0, 87, "comedy", "Modern Times", null, 1936 },
                    { "MID167", 4.0999999999999996, 121, "comedy", "Some Like It Hot", null, 1959 },
                    { "MID168", 4.0, 120, "drama", "Do the Right Thing", null, 1989 },
                    { "MID169", 3.8999999999999999, 128, "drama", "Selma", null, 2014 },
                    { "MID170", 3.8999999999999999, 134, "romance", "Brokeback Mountain", null, 2005 },
                    { "MID171", 4.0, 111, "drama", "The Piano", null, 1993 },
                    { "MID172", 4.0, 134, "drama", "The Remains of the Day", null, 1993 },
                    { "MID173", 3.8999999999999999, 114, "comedy", "Adaptation.", null, 2002 },
                    { "MID174", 3.7999999999999998, 124, "thriller", "Match Point", null, 2005 },
                    { "MID175", 3.8999999999999999, 170, "war", "The Thin Red Line", null, 1998 },
                    { "MID176", 4.0, 88, "war", "Paths of Glory", null, 1957 },
                    { "MID177", 3.8999999999999999, 138, "thriller", "The Wages of Fear", null, 1953 },
                    { "MID178", 3.8999999999999999, 172, "drama", "The Best Years of Our Lives", null, 1946 },
                    { "MID179", 3.8999999999999999, 112, "comedy", "The Philadelphia Story", null, 1940 },
                    { "MID180", 4.0, 96, "drama", "The Passion of Joan of Arc", null, 1928 },
                    { "MID181", 3.7999999999999998, 103, "romance", "Wuthering Heights", null, 1939 },
                    { "MID182", 3.8999999999999999, 130, "mystery", "Rebecca", null, 1940 },
                    { "MID183", 3.8999999999999999, 113, "thriller", "The Conversation", null, 1974 },
                    { "MID184", 3.7000000000000002, 88, "drama", "The Magnificent Ambersons", null, 1942 },
                    { "MID185", 3.7999999999999998, 144, "mystery", "L'Avventura", null, 1960 },
                    { "MID186", 3.8999999999999999, 170, "musical", "My Fair Lady", null, 1964 },
                    { "MID187", 3.7999999999999998, 153, "musical", "Oliver!", null, 1968 },
                    { "MID188", 3.8999999999999999, 124, "musical", "Cabaret", null, 1972 },
                    { "MID189", 3.7999999999999998, 120, "drama", "Sound of Metal", null, 2019 },
                    { "MID190", 3.7999999999999998, 108, "drama", "Nomadland", null, 2020 },
                    { "MID191", 3.7999999999999998, 113, "thriller", "Promising Young Woman", null, 2020 },
                    { "MID192", 3.7999999999999998, 115, "drama", "Minari", null, 2020 },
                    { "MID193", 3.7999999999999998, 130, "drama", "The Trial of the Chicago 7", null, 2020 },
                    { "MID194", 4.0, 100, "animation", "Soul", null, 2020 },
                    { "MID195", 3.7000000000000002, 150, "sci-fi", "Tenet", null, 2020 },
                    { "MID196", 3.8999999999999999, 155, "sci-fi", "Dune", null, 2021 },
                    { "MID197", 3.8999999999999999, 126, "drama", "The Power of the Dog", null, 2021 },
                    { "MID198", 3.8999999999999999, 111, "drama", "CODA", null, 2021 },
                    { "MID199", 3.7999999999999998, 156, "musical", "West Side Story", null, 2021 },
                    { "MID200", 3.8999999999999999, 179, "drama", "Drive My Car", null, 2021 },
                    { "MID201", 3.8999999999999999, 139, "romance", "Decision to Leave", null, 2022 },
                    { "MID202", 3.6000000000000001, 147, "satire", "Triangle of Sadness", null, 2022 },
                    { "MID203", 3.8999999999999999, 147, "war", "All Quiet on the Western Front (2022)", null, 2022 },
                    { "MID204", 3.7000000000000002, 114, "comedy", "Barbie", null, 2023 },
                    { "MID205", 4.2000000000000002, 180, "drama", "Oppenheimer", null, 2023 },
                    { "MID206", 4.0, 105, "romance", "Past Lives", null, 2023 },
                    { "MID207", 4.2000000000000002, 140, "animation", "Spider-Man: Across the Spider-Verse", null, 2023 },
                    { "MID208", 3.7000000000000002, 130, "drama", "The Zone of Interest", null, 2023 },
                    { "MID209", 3.7999999999999998, 165, "mystery", "Anatomy of a Fall", null, 2023 },
                    { "MID210", 3.7999999999999998, 150, "fantasy", "Poor Things", null, 2023 },
                    { "MID211", 3.6000000000000001, 108, "comedy", "Next Goal Wins", null, 2023 },
                    { "MID212", 3.5, 140, "drama", "Saltburn", null, 2023 },
                    { "MID213", 3.7999999999999998, 130, "drama", "The Holdovers", null, 2023 },
                    { "MID214", 3.7999999999999998, 206, "crime", "Killers of the Flower Moon", null, 2023 },
                    { "MID215", 3.6000000000000001, 140, "drama", "May December", null, 2023 },
                    { "MID216", 3.7000000000000002, 128, "drama", "American Fiction", null, 2023 },
                    { "MID217", 3.6000000000000001, 135, "drama", "The Color Purple (2023)", null, 2023 },
                    { "MID218", 3.3999999999999999, 112, "sci-fi", "The Creator", null, 2023 },
                    { "MID219", 3.6000000000000001, 137, "drama", "The Iron Claw", null, 2023 },
                    { "MID220", 3.5, 120, "drama", "Are You There God? It's Me, Margaret.", null, 2023 },
                    { "MID221", 3.5, 100, "drama", "Untitled Film 221", null, 2000 },
                    { "MID222", 3.5, 100, "drama", "Untitled Film 222", null, 2000 },
                    { "MID223", 3.5, 100, "drama", "Untitled Film 223", null, 2000 },
                    { "MID224", 3.5, 100, "drama", "Untitled Film 224", null, 2000 },
                    { "MID225", 3.5, 100, "drama", "Untitled Film 225", null, 2000 },
                    { "MID226", 3.5, 100, "drama", "Untitled Film 226", null, 2000 },
                    { "MID227", 3.5, 100, "drama", "Untitled Film 227", null, 2000 },
                    { "MID228", 3.5, 100, "drama", "Untitled Film 228", null, 2000 },
                    { "MID229", 3.5, 100, "drama", "Untitled Film 229", null, 2000 },
                    { "MID230", 3.5, 100, "drama", "Untitled Film 230", null, 2000 },
                    { "MID231", 3.5, 100, "drama", "Untitled Film 231", null, 2000 },
                    { "MID232", 3.5, 100, "drama", "Untitled Film 232", null, 2000 },
                    { "MID233", 3.5, 100, "drama", "Untitled Film 233", null, 2000 },
                    { "MID234", 3.5, 100, "drama", "Untitled Film 234", null, 2000 },
                    { "MID235", 3.5, 100, "drama", "Untitled Film 235", null, 2000 },
                    { "MID236", 3.5, 100, "drama", "Untitled Film 236", null, 2000 },
                    { "MID237", 3.5, 100, "drama", "Untitled Film 237", null, 2000 },
                    { "MID238", 3.5, 100, "drama", "Untitled Film 238", null, 2000 },
                    { "MID239", 3.5, 100, "drama", "Untitled Film 239", null, 2000 },
                    { "MID240", 3.5, 100, "drama", "Untitled Film 240", null, 2000 },
                    { "MID241", 3.5, 100, "drama", "Untitled Film 241", null, 2000 },
                    { "MID242", 3.5, 100, "drama", "Untitled Film 242", null, 2000 },
                    { "MID243", 3.5, 100, "drama", "Untitled Film 243", null, 2000 },
                    { "MID244", 3.5, 100, "drama", "Untitled Film 244", null, 2000 },
                    { "MID245", 3.5, 100, "drama", "Untitled Film 245", null, 2000 },
                    { "MID246", 3.5, 100, "drama", "Untitled Film 246", null, 2000 },
                    { "MID247", 3.5, 100, "drama", "Untitled Film 247", null, 2000 },
                    { "MID248", 3.5, 100, "drama", "Untitled Film 248", null, 2000 },
                    { "MID249", 3.5, 100, "drama", "Untitled Film 249", null, 2000 },
                    { "MID250", 3.5, 100, "drama", "Untitled Film 250", null, 2000 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "bio", "diary_id", "email", "id", "list_id", "username", "watched_id", "WatchedId1", "watchlist_id" },
                values: new object[,]
                {
                    { "UID001", "Film buff, coffee addict, aspiring screenwriter. Always searching for the next mind-bending thriller.", null, "sam1@letterbox.test", null, null, "sam", null, null, null },
                    { "UID002", "Loves indie films, hiking, and spontaneous road trips. Can quote Tarantino on a Tuesday.", null, "jordan2@letterbox.test", null, null, "jordan", null, null, null },
                    { "UID003", "Animation enthusiast, board game champion, and collector of obscure movie trivia.", null, "kristian3@letterbox.test", null, null, "kristian", null, null, null },
                    { "UID004", "Late-night movie watcher. Believes popcorn is a food group and that 2 a.m. is peak cinema time.", null, "nocturne_curious4@letterbox.test", null, null, "nocturne_curious", null, null, null },
                    { "UID005", "Cinephile with a soft spot for 80s soundtracks and epic fantasy sagas.", null, "eighties_heart5@letterbox.test", null, null, "eighties_heart", null, null, null },
                    { "UID006", "Documentary lover, amateur photographer, festival regular—asks too many questions during Q&As.", null, "docu_hoarder6@letterbox.test", null, null, "docu_hoarder", null, null, null },
                    { "UID007", "Always up for a rom-com marathon. Writes reviews using too many heart emojis.", null, "romcom_ronin7@letterbox.test", null, null, "romcom_ronin", null, null, null },
                    { "UID008", "Action junkie, sci-fi dreamer, self-proclaimed movie quote master—owns a themed jacket.", null, "action_alex8@letterbox.test", null, null, "action_alex", null, null, null },
                    { "UID009", "Traveler and cinephile. Watches foreign films on trains and rates snacks by region.", null, "wanderlust_wren9@letterbox.test", null, null, "wanderlust_wren", null, null, null },
                    { "UID010", "Enjoys foreign films, spicy food, and debating plot twists at midnight.", null, "spicy_subtitles10@letterbox.test", null, null, "spicy_subtitles", null, null, null },
                    { "UID011", "Classic cinema devotee. Can recite Casablanca, but only the good lines.", null, "classic_cleo11@letterbox.test", null, null, "classic_cleo", null, null, null },
                    { "UID012", "Indie filmmaker who takes stills of the credits and frames them at home.", null, "pixel_popcorn12@letterbox.test", null, null, "pixel_popcorn", null, null, null },
                    { "UID013", "Quiet critic: short, sharp reviews. Prefers rainy-day cinema.", null, "quiet_projection13@letterbox.test", null, null, "quiet_projection", null, null, null },
                    { "UID014", "Basement projector owner. Hosts monthly midnight screenings with suspiciously good nachos.", null, "film_basement14@letterbox.test", null, null, "film_basement", null, null, null },
                    { "UID015", "Collector of mixtape soundtracks and late-run double features.", null, "cassette_cinema15@letterbox.test", null, null, "cassette_cinema", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "fav",
                columns: new[] { "fav_id", "movie_id" },
                values: new object[,]
                {
                    { "FID001", "MID001" },
                    { "FID002", "MID024" },
                    { "FID003", "MID004" },
                    { "FID004", "MID027" },
                    { "FID005", "MID007" },
                    { "FID006", "MID030" },
                    { "FID007", "MID010" },
                    { "FID008", "MID033" },
                    { "FID009", "MID013" },
                    { "FID010", "MID036" },
                    { "FID011", "MID016" },
                    { "FID012", "MID039" },
                    { "FID013", "MID019" },
                    { "FID014", "MID042" },
                    { "FID015", "MID022" },
                    { "FID016", "MID045" },
                    { "FID017", "MID025" },
                    { "FID018", "MID048" },
                    { "FID019", "MID028" },
                    { "FID020", "MID051" },
                    { "FID021", "MID031" },
                    { "FID022", "MID054" },
                    { "FID023", "MID034" },
                    { "FID024", "MID057" },
                    { "FID025", "MID037" },
                    { "FID026", "MID060" },
                    { "FID027", "MID040" },
                    { "FID028", "MID063" },
                    { "FID029", "MID043" },
                    { "FID030", "MID066" }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "rating_id", "id", "movie_name", "review", "stars", "user_id" },
                values: new object[,]
                {
                    { "RID001", null, null, null, null, "UID001" },
                    { "RID002", null, null, null, null, "UID001" },
                    { "RID003", null, null, null, null, "UID001" },
                    { "RID004", null, null, null, null, "UID002" },
                    { "RID005", null, null, null, null, "UID002" },
                    { "RID006", null, null, null, null, "UID002" },
                    { "RID007", null, null, null, null, "UID003" },
                    { "RID008", null, null, null, null, "UID003" },
                    { "RID009", null, null, null, null, "UID003" },
                    { "RID010", null, null, null, null, "UID004" },
                    { "RID011", null, null, null, null, "UID004" },
                    { "RID012", null, null, null, null, "UID004" },
                    { "RID013", null, null, null, null, "UID005" },
                    { "RID014", null, null, null, null, "UID005" },
                    { "RID015", null, null, null, null, "UID005" },
                    { "RID016", null, null, null, null, "UID006" },
                    { "RID017", null, null, null, null, "UID006" },
                    { "RID018", null, null, null, null, "UID006" },
                    { "RID019", null, null, null, null, "UID007" },
                    { "RID020", null, null, null, null, "UID007" },
                    { "RID021", null, null, null, null, "UID007" },
                    { "RID022", null, null, null, null, "UID008" },
                    { "RID023", null, null, null, null, "UID008" },
                    { "RID024", null, null, null, null, "UID008" },
                    { "RID025", null, null, null, null, "UID009" },
                    { "RID026", null, null, null, null, "UID009" },
                    { "RID027", null, null, null, null, "UID009" },
                    { "RID028", null, null, null, null, "UID010" },
                    { "RID029", null, null, null, null, "UID010" },
                    { "RID030", null, null, null, null, "UID010" },
                    { "RID031", null, null, null, null, "UID011" },
                    { "RID032", null, null, null, null, "UID011" },
                    { "RID033", null, null, null, null, "UID011" },
                    { "RID034", null, null, null, null, "UID012" },
                    { "RID035", null, null, null, null, "UID012" },
                    { "RID036", null, null, null, null, "UID012" },
                    { "RID037", null, null, null, null, "UID013" },
                    { "RID038", null, null, null, null, "UID013" },
                    { "RID039", null, null, null, null, "UID013" },
                    { "RID040", null, null, null, null, "UID014" },
                    { "RID041", null, null, null, null, "UID014" },
                    { "RID042", null, null, null, null, "UID014" },
                    { "RID043", null, null, null, null, "UID015" },
                    { "RID044", null, null, null, null, "UID015" },
                    { "RID045", null, null, null, null, "UID015" }
                });

            migrationBuilder.InsertData(
                table: "watched",
                columns: new[] { "id", "diary_id", "fav_id", "movie_id", "user_id" },
                values: new object[,]
                {
                    { "ZID0001", null, null, "MID001", "UID001" },
                    { "ZID0002", null, null, "MID018", "UID001" },
                    { "ZID0003", null, null, "MID035", "UID001" },
                    { "ZID0004", null, null, "MID052", "UID001" },
                    { "ZID0005", null, null, "MID069", "UID001" },
                    { "ZID0006", null, null, "MID086", "UID001" },
                    { "ZID0007", null, null, "MID010", "UID002" },
                    { "ZID0008", null, null, "MID027", "UID002" },
                    { "ZID0009", null, null, "MID044", "UID002" },
                    { "ZID0010", null, null, "MID061", "UID002" },
                    { "ZID0011", null, null, "MID078", "UID002" },
                    { "ZID0012", null, null, "MID095", "UID002" },
                    { "ZID0013", null, null, "MID019", "UID003" },
                    { "ZID0014", null, null, "MID036", "UID003" },
                    { "ZID0015", null, null, "MID053", "UID003" },
                    { "ZID0016", null, null, "MID070", "UID003" },
                    { "ZID0017", null, null, "MID087", "UID003" },
                    { "ZID0018", null, null, "MID104", "UID003" },
                    { "ZID0019", null, null, "MID028", "UID004" },
                    { "ZID0020", null, null, "MID045", "UID004" },
                    { "ZID0021", null, null, "MID062", "UID004" },
                    { "ZID0022", null, null, "MID079", "UID004" },
                    { "ZID0023", null, null, "MID096", "UID004" },
                    { "ZID0024", null, null, "MID113", "UID004" },
                    { "ZID0025", null, null, "MID037", "UID005" },
                    { "ZID0026", null, null, "MID054", "UID005" },
                    { "ZID0027", null, null, "MID071", "UID005" },
                    { "ZID0028", null, null, "MID088", "UID005" },
                    { "ZID0029", null, null, "MID105", "UID005" },
                    { "ZID0030", null, null, "MID122", "UID005" },
                    { "ZID0031", null, null, "MID046", "UID006" },
                    { "ZID0032", null, null, "MID063", "UID006" },
                    { "ZID0033", null, null, "MID080", "UID006" },
                    { "ZID0034", null, null, "MID097", "UID006" },
                    { "ZID0035", null, null, "MID114", "UID006" },
                    { "ZID0036", null, null, "MID131", "UID006" },
                    { "ZID0037", null, null, "MID055", "UID007" },
                    { "ZID0038", null, null, "MID072", "UID007" },
                    { "ZID0039", null, null, "MID089", "UID007" },
                    { "ZID0040", null, null, "MID106", "UID007" },
                    { "ZID0041", null, null, "MID123", "UID007" },
                    { "ZID0042", null, null, "MID140", "UID007" },
                    { "ZID0043", null, null, "MID064", "UID008" },
                    { "ZID0044", null, null, "MID081", "UID008" },
                    { "ZID0045", null, null, "MID098", "UID008" },
                    { "ZID0046", null, null, "MID115", "UID008" },
                    { "ZID0047", null, null, "MID132", "UID008" },
                    { "ZID0048", null, null, "MID149", "UID008" },
                    { "ZID0049", null, null, "MID073", "UID009" },
                    { "ZID0050", null, null, "MID090", "UID009" },
                    { "ZID0051", null, null, "MID107", "UID009" },
                    { "ZID0052", null, null, "MID124", "UID009" },
                    { "ZID0053", null, null, "MID141", "UID009" },
                    { "ZID0054", null, null, "MID158", "UID009" },
                    { "ZID0055", null, null, "MID082", "UID010" },
                    { "ZID0056", null, null, "MID099", "UID010" },
                    { "ZID0057", null, null, "MID116", "UID010" },
                    { "ZID0058", null, null, "MID133", "UID010" },
                    { "ZID0059", null, null, "MID150", "UID010" },
                    { "ZID0060", null, null, "MID167", "UID010" },
                    { "ZID0061", null, null, "MID091", "UID011" },
                    { "ZID0062", null, null, "MID108", "UID011" },
                    { "ZID0063", null, null, "MID125", "UID011" },
                    { "ZID0064", null, null, "MID142", "UID011" },
                    { "ZID0065", null, null, "MID159", "UID011" },
                    { "ZID0066", null, null, "MID176", "UID011" },
                    { "ZID0067", null, null, "MID100", "UID012" },
                    { "ZID0068", null, null, "MID117", "UID012" },
                    { "ZID0069", null, null, "MID134", "UID012" },
                    { "ZID0070", null, null, "MID151", "UID012" },
                    { "ZID0071", null, null, "MID168", "UID012" },
                    { "ZID0072", null, null, "MID185", "UID012" },
                    { "ZID0073", null, null, "MID109", "UID013" },
                    { "ZID0074", null, null, "MID126", "UID013" },
                    { "ZID0075", null, null, "MID143", "UID013" },
                    { "ZID0076", null, null, "MID160", "UID013" },
                    { "ZID0077", null, null, "MID177", "UID013" },
                    { "ZID0078", null, null, "MID194", "UID013" },
                    { "ZID0079", null, null, "MID118", "UID014" },
                    { "ZID0080", null, null, "MID135", "UID014" },
                    { "ZID0081", null, null, "MID152", "UID014" },
                    { "ZID0082", null, null, "MID169", "UID014" },
                    { "ZID0083", null, null, "MID186", "UID014" },
                    { "ZID0084", null, null, "MID203", "UID014" },
                    { "ZID0085", null, null, "MID127", "UID015" },
                    { "ZID0086", null, null, "MID144", "UID015" },
                    { "ZID0087", null, null, "MID161", "UID015" },
                    { "ZID0088", null, null, "MID178", "UID015" },
                    { "ZID0089", null, null, "MID195", "UID015" },
                    { "ZID0090", null, null, "MID212", "UID015" }
                });

            migrationBuilder.InsertData(
                table: "watchlist",
                columns: new[] { "watchlist_id", "id", "movie_id" },
                values: new object[,]
                {
                    { "WID001", null, "MID001" },
                    { "WID002", null, "MID014" },
                    { "WID003", null, "MID027" },
                    { "WID004", null, "MID006" },
                    { "WID005", null, "MID019" },
                    { "WID006", null, "MID032" },
                    { "WID007", null, "MID011" },
                    { "WID008", null, "MID024" },
                    { "WID009", null, "MID037" },
                    { "WID010", null, "MID016" },
                    { "WID011", null, "MID029" },
                    { "WID012", null, "MID042" },
                    { "WID013", null, "MID021" },
                    { "WID014", null, "MID034" },
                    { "WID015", null, "MID047" },
                    { "WID016", null, "MID026" },
                    { "WID017", null, "MID039" },
                    { "WID018", null, "MID052" },
                    { "WID019", null, "MID031" },
                    { "WID020", null, "MID044" },
                    { "WID021", null, "MID057" },
                    { "WID022", null, "MID036" },
                    { "WID023", null, "MID049" },
                    { "WID024", null, "MID062" },
                    { "WID025", null, "MID041" },
                    { "WID026", null, "MID054" },
                    { "WID027", null, "MID067" },
                    { "WID028", null, "MID046" },
                    { "WID029", null, "MID059" },
                    { "WID030", null, "MID072" },
                    { "WID031", null, "MID051" },
                    { "WID032", null, "MID064" },
                    { "WID033", null, "MID077" },
                    { "WID034", null, "MID056" },
                    { "WID035", null, "MID069" },
                    { "WID036", null, "MID082" },
                    { "WID037", null, "MID061" },
                    { "WID038", null, "MID074" },
                    { "WID039", null, "MID087" },
                    { "WID040", null, "MID066" },
                    { "WID041", null, "MID079" },
                    { "WID042", null, "MID092" },
                    { "WID043", null, "MID071" },
                    { "WID044", null, "MID084" },
                    { "WID045", null, "MID097" }
                });

            migrationBuilder.InsertData(
                table: "diary",
                columns: new[] { "diary_id", "movie_id", "rating_id" },
                values: new object[,]
                {
                    { "DID001", "MID001", "RID001" },
                    { "DID002", "MID023", "RID003" },
                    { "DID003", "MID008", "RID004" },
                    { "DID004", "MID030", "RID006" },
                    { "DID005", "MID015", "RID007" },
                    { "DID006", "MID037", "RID009" },
                    { "DID007", "MID022", "RID010" },
                    { "DID008", "MID044", "RID012" },
                    { "DID009", "MID029", "RID013" },
                    { "DID010", "MID051", "RID015" },
                    { "DID011", "MID036", "RID016" },
                    { "DID012", "MID058", "RID018" },
                    { "DID013", "MID043", "RID019" },
                    { "DID014", "MID065", "RID021" },
                    { "DID015", "MID050", "RID022" },
                    { "DID016", "MID072", "RID024" },
                    { "DID017", "MID057", "RID025" },
                    { "DID018", "MID079", "RID027" },
                    { "DID019", "MID064", "RID028" },
                    { "DID020", "MID086", "RID030" },
                    { "DID021", "MID071", "RID031" },
                    { "DID022", "MID093", "RID033" },
                    { "DID023", "MID078", "RID034" },
                    { "DID024", "MID100", "RID036" },
                    { "DID025", "MID085", "RID037" },
                    { "DID026", "MID107", "RID039" },
                    { "DID027", "MID092", "RID040" },
                    { "DID028", "MID114", "RID042" },
                    { "DID029", "MID099", "RID043" },
                    { "DID030", "MID121", "RID045" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_diary_movie_id",
                table: "diary",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_diary_rating_id",
                table: "diary",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_fav_movie_id",
                table: "fav",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_rating_id",
                table: "movies",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_user_id",
                table: "ratings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_diary_id",
                table: "users",
                column: "diary_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_WatchedId1",
                table: "users",
                column: "WatchedId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_watchlist_id",
                table: "users",
                column: "watchlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_watched_movie_id",
                table: "watched",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_watchlist_movie_id",
                table: "watchlist",
                column: "movie_id");

            migrationBuilder.AddForeignKey(
                name: "FK_diary_movies_movie_id",
                table: "diary",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_diary_ratings_rating_id",
                table: "diary",
                column: "rating_id",
                principalTable: "ratings",
                principalColumn: "rating_id");

            migrationBuilder.AddForeignKey(
                name: "FK_fav_movies_movie_id",
                table: "fav",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_movies_ratings_rating_id",
                table: "movies",
                column: "rating_id",
                principalTable: "ratings",
                principalColumn: "rating_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_diary_movies_movie_id",
                table: "diary");

            migrationBuilder.DropForeignKey(
                name: "FK_watched_movies_movie_id",
                table: "watched");

            migrationBuilder.DropForeignKey(
                name: "FK_watchlist_movies_movie_id",
                table: "watchlist");

            migrationBuilder.DropForeignKey(
                name: "FK_diary_ratings_rating_id",
                table: "diary");

            migrationBuilder.DropTable(
                name: "fav");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "diary");

            migrationBuilder.DropTable(
                name: "watched");

            migrationBuilder.DropTable(
                name: "watchlist");
        }
    }
}
