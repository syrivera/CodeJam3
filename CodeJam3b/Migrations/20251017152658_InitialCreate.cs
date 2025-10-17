using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    id = table.Column<string>(type: "text", nullable: false),
                    rating_id = table.Column<string>(type: "text", nullable: true),
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
                principalColumn: "id");

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
                principalColumn: "id");
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
