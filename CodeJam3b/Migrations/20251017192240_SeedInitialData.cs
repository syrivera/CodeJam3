using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeJam3b.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_diary_diary_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_watched_WatchedId1",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_watchlist_watchlist_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watchlist",
                table: "watchlist");

            migrationBuilder.DropIndex(
                name: "IX_users_diary_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_WatchedId1",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_watchlist_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fav",
                table: "fav");

            migrationBuilder.DropPrimaryKey(
                name: "PK_diary",
                table: "diary");

            migrationBuilder.DropColumn(
                name: "WatchedId1",
                table: "users");

            migrationBuilder.DropColumn(
                name: "id",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "watchlist",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "rating_id",
                table: "ratings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "fav",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "diary",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_watchlist",
                table: "watchlist",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fav",
                table: "fav",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_diary",
                table: "diary",
                column: "id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_watchlist",
                table: "watchlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fav",
                table: "fav");

            migrationBuilder.DropPrimaryKey(
                name: "PK_diary",
                table: "diary");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "diary",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "9");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "fav",
                keyColumn: "id",
                keyColumnType: "text",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-001");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-002");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-003");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-004");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-005");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-006");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-007");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-008");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-009");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-010");

            migrationBuilder.DeleteData(
                table: "watched",
                keyColumn: "id",
                keyValue: "watched-entry-011");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "watchlist",
                keyColumn: "id",
                keyValue: "9");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-001");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-002");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-003");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-004");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-005");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-006");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-007");

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "id",
                keyValue: "movie-008");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-001");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-002");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-003");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-004");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-005");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-006");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-007");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-008");

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: "rating-009");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: "user-001");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: "user-002");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: "user-003");

            migrationBuilder.DropColumn(
                name: "id",
                table: "fav");

            migrationBuilder.DropColumn(
                name: "id",
                table: "diary");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "watchlist",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "WatchedId1",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "rating_id",
                table: "ratings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_watchlist",
                table: "watchlist",
                column: "watchlist_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fav",
                table: "fav",
                column: "fav_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_diary",
                table: "diary",
                column: "diary_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_users_diary_diary_id",
                table: "users",
                column: "diary_id",
                principalTable: "diary",
                principalColumn: "diary_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_watched_WatchedId1",
                table: "users",
                column: "WatchedId1",
                principalTable: "watched",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_watchlist_watchlist_id",
                table: "users",
                column: "watchlist_id",
                principalTable: "watchlist",
                principalColumn: "watchlist_id");
        }
    }
}
