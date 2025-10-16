using Microsoft.EntityFrameworkCore;


public class SchoolDbContext(string dbName = "sis") : DbContext // sis = school information system
{


    private readonly string _connectionHost = "localhost";
    private readonly string _connectionDbName = dbName;

    public DbSet<Student> Students { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={_connectionHost};Database={_connectionDbName}");
        base.OnConfiguring(optionsBuilder);
    }
}
