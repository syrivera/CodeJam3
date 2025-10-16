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

        // Parameterless constructor used by Program.cs (new LetterBoxDbContext())
        public LetterBoxDbContext() : this("letterbox") { }

        // Named-constructor for explicit DB name usage
        public LetterBoxDbContext(string dbName)
        {
            _connectionDbName = dbName ?? "letterbox";
        }

        // Options constructor for DI/EF tooling
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
            // Only configure here if no options have been provided (e.g. when created directly)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={_connectionHost};Database={_connectionDbName}");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Watched -> User relationship explicitly to avoid ambiguous one-to-one detection.
            // Watched.UserId is a string that corresponds to User.Id (also string).
            modelBuilder.Entity<CodeJam3b.Models.Lists.Watched>()
                .HasOne<CodeJam3b.Models.Users.User>()
                .WithMany(u => u.Watched)
                .HasForeignKey(w => w.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.SetNull);

            // Diary.RatingId (string) maps to Rating.RatingId (string) - configure explicitly
            modelBuilder.Entity<CodeJam3b.Models.Lists.Diary>()
                .HasOne<CodeJam3b.Models.Movies.Rating>(d => d.Rating)
                .WithMany()
                .HasForeignKey(d => d.RatingId)
                .HasPrincipalKey(r => r.RatingId)
                .OnDelete(DeleteBehavior.SetNull);

            // Rating.UserId (string) maps to User.Id (string) - configure explicitly
            modelBuilder.Entity<CodeJam3b.Models.Movies.Rating>()
                .HasOne<CodeJam3b.Models.Users.User>(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
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
