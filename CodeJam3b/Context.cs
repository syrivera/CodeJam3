using CodeJam3b.Models.Lists;
using CodeJam3b.Models.Movies;
using CodeJam3b.Models.Users;
using Microsoft.EntityFrameworkCore;


public class SchoolDbContext(string dbName = "sis") : DbContext // sis = school information system
{


    private readonly string _connectionHost = "localhost";
    private readonly string _connectionDbName = dbName;

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Diary> Diaries { get; set; }
    public DbSet<Fav> Favs { get; set; }
    public DbSet<Watched> Watched { get; set; } 
    public DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={_connectionHost};Database={_connectionDbName}");
        base.OnConfiguring(optionsBuilder);
    }
}
