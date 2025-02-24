using GameAPI.Classes;
using Microsoft.EntityFrameworkCore;

namespace GameAPI;

public class GameDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GameGenre> GameGenres { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }
    public DbSet<GameDeveloper> GameDevelopers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<GameReviews> GameReviews { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Reviewever> Reviewevers { get; set; }

    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameGenre>().HasKey(gameGenre => new { gameGenre.GameId, gameGenre.GenreId });
        modelBuilder.Entity<GamePlatform>().HasKey(gamePlatform => new { gamePlatform.GameId, gamePlatform.PlatformId });
        modelBuilder.Entity<GameDeveloper>().HasKey(gameDeveloper => new { gameDeveloper.GameId, gameDeveloper.DeveloperId });
        modelBuilder.Entity<GameReviews>().HasKey(gameReview => new { gameReview.GameId, gameReview.ReviewId });
        
        modelBuilder.Entity<GameReviews>()
            .HasOne(gr => gr.Game)
            .WithMany(g => g.GameReviews)
            .HasForeignKey(gr => gr.GameId);

        modelBuilder.Entity<GameReviews>()
            .HasOne(gr => gr.Review)
            .WithMany(r => r.GameReviews)
            .HasForeignKey(gr => gr.ReviewId);
        

    }
}