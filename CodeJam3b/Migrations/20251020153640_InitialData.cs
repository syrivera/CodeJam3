using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeJam3b.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    watched_id = table.Column<string>(type: "text", nullable: true),
                    list_id = table.Column<string>(type: "text", nullable: true),
                    watchlist_id = table.Column<string>(type: "text", nullable: true),
                    diary_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    rating_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true),
                    movie_name = table.Column<string>(type: "text", nullable: true),
                    stars = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_ratings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
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
                    table.ForeignKey(
                        name: "FK_movies_ratings_rating_id",
                        column: x => x.rating_id,
                        principalTable: "ratings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "diary",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    diary_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: true),
                    rating_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diary", x => x.id);
                    table.ForeignKey(
                        name: "FK_diary_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_diary_ratings_rating_id",
                        column: x => x.rating_id,
                        principalTable: "ratings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fav",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    fav_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fav", x => x.id);
                    table.ForeignKey(
                        name: "FK_fav_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
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
                    id = table.Column<string>(type: "text", nullable: false),
                    watchlist_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watchlist", x => x.id);
                    table.ForeignKey(
                        name: "FK_watchlist_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "id", "avg_rating", "duration_mins", "genre", "name", "rating_id", "release_year" },
                values: new object[,]
                {
                    { "movie-001", 4.7999999999999998, 142, "Drama", "The Shawshank Redemption", null, 1994 },
                    { "movie-002", 4.7000000000000002, 175, "Crime", "The Godfather", null, 1972 },
                    { "movie-003", 4.5, 148, "Sci-Fi", "Inception", null, 2010 },
                    { "movie-004", 4.5999999999999996, 154, "Crime", "Pulp Fiction", null, 1994 },
                    { "movie-005", 4.4000000000000004, 136, "Sci-Fi", "The Matrix", null, 1999 },
                    { "movie-006", 4.2999999999999998, 169, "Sci-Fi", "Interstellar", null, 2014 },
                    { "movie-007", 4.5, 139, "Drama", "Fight Club", null, 1999 },
                    { "movie-008", 4.5999999999999996, 142, "Drama", "Forrest Gump", null, 1994 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "bio", "diary_id", "email", "list_id", "username", "watched_id", "watchlist_id" },
                values: new object[,]
                {
                    { "user-001", "Love classic films and sci-fi", "diary-001", "john@example.com", null, "moviebuff123", "watched-001", "watchlist-001" },
                    { "user-002", "Drama enthusiast and film critic", "diary-002", "sarah@example.com", null, "cinephile", "watched-002", "watchlist-002" },
                    { "user-003", "Watching everything from classics to modern masterpieces", "diary-003", "mike@example.com", null, "filmfanatic", "watched-003", "watchlist-003" }
                });

            migrationBuilder.InsertData(
                table: "fav",
                columns: new[] { "id", "fav_id", "movie_id" },
                values: new object[,]
                {
                    { "1", "fav-001", "movie-001" },
                    { "2", "fav-001", "movie-003" },
                    { "3", "fav-002", "movie-002" },
                    { "4", "fav-002", "movie-007" },
                    { "5", "fav-003", "movie-008" },
                    { "6", "fav-003", "movie-001" }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "id", "movie_name", "rating_id", "review", "stars", "user_id" },
                values: new object[,]
                {
                    { "rating-001", "The Shawshank Redemption", "rating-001", "Absolute masterpiece! Tim Robbins and Morgan Freeman deliver unforgettable performances.", 5, "user-001" },
                    { "rating-002", "Inception", "rating-002", "Mind-bending brilliance. Nolan at his best!", 5, "user-001" },
                    { "rating-003", "The Matrix", "rating-003", "Revolutionary sci-fi that still holds up today.", 4, "user-001" },
                    { "rating-004", "The Godfather", "rating-004", "The pinnacle of cinema. Every scene is perfection.", 5, "user-002" },
                    { "rating-005", "Pulp Fiction", "rating-005", "Tarantino's masterclass in non-linear storytelling.", 4, "user-002" },
                    { "rating-006", "Fight Club", "rating-006", "Dark, twisted, and absolutely brilliant.", 5, "user-002" },
                    { "rating-007", "Interstellar", "rating-007", "Visually stunning with an emotional core.", 4, "user-003" },
                    { "rating-008", "Forrest Gump", "rating-008", "Heartwarming journey through American history.", 5, "user-003" },
                    { "rating-009", "The Shawshank Redemption", "rating-009", "Hope is a good thing, maybe the best of things.", 5, "user-003" }
                });

            migrationBuilder.InsertData(
                table: "watched",
                columns: new[] { "id", "diary_id", "fav_id", "movie_id", "user_id" },
                values: new object[,]
                {
                    { "watched-entry-001", "diary-001", "fav-001", "movie-001", "user-001" },
                    { "watched-entry-002", "diary-001", "fav-001", "movie-003", "user-001" },
                    { "watched-entry-003", "diary-001", null, "movie-005", "user-001" },
                    { "watched-entry-004", null, null, "movie-002", "user-001" },
                    { "watched-entry-005", "diary-002", "fav-002", "movie-002", "user-002" },
                    { "watched-entry-006", "diary-002", null, "movie-004", "user-002" },
                    { "watched-entry-007", "diary-002", "fav-002", "movie-007", "user-002" },
                    { "watched-entry-008", null, null, "movie-001", "user-002" },
                    { "watched-entry-009", "diary-003", null, "movie-006", "user-003" },
                    { "watched-entry-010", "diary-003", "fav-003", "movie-008", "user-003" },
                    { "watched-entry-011", "diary-003", "fav-003", "movie-001", "user-003" }
                });

            migrationBuilder.InsertData(
                table: "watchlist",
                columns: new[] { "id", "movie_id", "watchlist_id" },
                values: new object[,]
                {
                    { "1", "movie-004", "watchlist-001" },
                    { "2", "movie-006", "watchlist-001" },
                    { "3", "movie-007", "watchlist-001" },
                    { "4", "movie-003", "watchlist-002" },
                    { "5", "movie-005", "watchlist-002" },
                    { "6", "movie-006", "watchlist-002" },
                    { "7", "movie-002", "watchlist-003" },
                    { "8", "movie-004", "watchlist-003" },
                    { "9", "movie-007", "watchlist-003" }
                });

            migrationBuilder.InsertData(
                table: "diary",
                columns: new[] { "id", "diary_id", "movie_id", "rating_id" },
                values: new object[,]
                {
                    { "1", "diary-001", "movie-001", "rating-001" },
                    { "2", "diary-001", "movie-003", "rating-002" },
                    { "3", "diary-001", "movie-005", "rating-003" },
                    { "4", "diary-002", "movie-002", "rating-004" },
                    { "5", "diary-002", "movie-004", "rating-005" },
                    { "6", "diary-002", "movie-007", "rating-006" },
                    { "7", "diary-003", "movie-006", "rating-007" },
                    { "8", "diary-003", "movie-008", "rating-008" },
                    { "9", "diary-003", "movie-001", "rating-009" }
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
                name: "IX_watched_movie_id",
                table: "watched",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_watchlist_movie_id",
                table: "watchlist",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diary");

            migrationBuilder.DropTable(
                name: "fav");

            migrationBuilder.DropTable(
                name: "watched");

            migrationBuilder.DropTable(
                name: "watchlist");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
