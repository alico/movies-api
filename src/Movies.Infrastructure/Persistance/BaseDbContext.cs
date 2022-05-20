using Microsoft.EntityFrameworkCore;
using Movies.Application.Commands.Configuration;
using Movies.Domain.Contracts;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Persistance;
public abstract class BaseDbContext : DbContext, IDbContext
{
    protected readonly IConfigurationManager _configurationManager;
    public BaseDbContext(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }
    public BaseDbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Movie>().HasKey(x => x.Id);
        modelBuilder.Entity<Genre>().HasKey(x => x.Id);
        modelBuilder.Entity<MovieGenre>().HasKey(x => x.Id);
        modelBuilder.Entity<Rating>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasKey(x => x.Id);

        modelBuilder.Entity<Movie>().HasMany(x => x.Ratings);
        modelBuilder.Entity<Movie>().HasMany(x => x.Genres);
        modelBuilder.Entity<User>().HasMany(x => x.Ratings);
        modelBuilder.Entity<Rating>().HasOne(x => x.Movie);
        modelBuilder.Entity<Rating>().HasOne(x => x.User);
        modelBuilder.Entity<MovieGenre>().HasOne(x => x.Genre);
        modelBuilder.Seed();
    }


    public bool EnsureDbCreated()
    {
        return this.Database.EnsureCreated();
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }


}
