using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using CodeJam3b.Models.Users;
using CodeJam3b.Models.Lists;
namespace CodeJam3b.Models.Movies;


public class LetterBoxDbContext(string dbName = "letterbox") : DbContext 
{


    private readonly string _connectionHost = "localhost";
    private readonly string _connectionDbName = dbName;

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Watchlist> Watchlist { get; set; }
    public DbSet<Watched> Watched { get; set; }
    public DbSet<Fav> Fav { get; set; }
    public DbSet<Diary> Diary { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
            "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=letterbox;Include Error Detail=true;");
        base.OnConfiguring(optionsBuilder);
    }
}
