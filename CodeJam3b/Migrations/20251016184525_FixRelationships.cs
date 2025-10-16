using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeJam3b.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "students");

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
                    rating_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "watchlist",
                columns: table => new
                {
                    watchlist_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: true),
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
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    watched_id = table.Column<string>(type: "text", nullable: true),
                    list_id = table.Column<Guid>(type: "uuid", nullable: true),
                    watchlist_id = table.Column<string>(type: "text", nullable: true),
                    diary_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                    table.UniqueConstraint("AK_user_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_diary_diary_id",
                        column: x => x.diary_id,
                        principalTable: "diary",
                        principalColumn: "diary_id");
                    table.ForeignKey(
                        name: "FK_user_watchlist_watchlist_id",
                        column: x => x.watchlist_id,
                        principalTable: "watchlist",
                        principalColumn: "watchlist_id");
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true),
                    movie_name = table.Column<string>(type: "text", nullable: true),
                    stars = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.id);
                    table.UniqueConstraint("AK_ratings_rating_id", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK_ratings_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "watched",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_watched_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "IX_user_diary_id",
                table: "user",
                column: "diary_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_watchlist_id",
                table: "user",
                column: "watchlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_watched_movie_id",
                table: "watched",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_watched_user_id",
                table: "watched",
                column: "user_id");

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
                principalColumn: "rating_id",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_watchlist_movies_movie_id",
                table: "watchlist");

            migrationBuilder.DropForeignKey(
                name: "FK_diary_ratings_rating_id",
                table: "diary");

            migrationBuilder.DropTable(
                name: "fav");

            migrationBuilder.DropTable(
                name: "watched");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "diary");

            migrationBuilder.DropTable(
                name: "watchlist");

            migrationBuilder.CreateTable(
                name: "assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaxScore = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignments", x => x.AssignmentId);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    GitHub = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssignmentId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_grades_assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grades_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grades_AssignmentId",
                table: "grades",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_grades_StudentId",
                table: "grades",
                column: "StudentId");
        }
    }
}
