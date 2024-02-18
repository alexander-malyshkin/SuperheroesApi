using Microsoft.EntityFrameworkCore;

namespace SuperHeroes.Repositories;

/// <summary>
/// Represents the superheroes database context
/// </summary>
public class SuperheroesDbContext : DbContext
{
    public SuperheroesDbContext(DbContextOptions<SuperheroesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // we are using a composite key for the user favourite superheroes
        modelBuilder.Entity<UserFavouriteSuperhero>()
            .HasKey(uf => new { uf.UserToken, uf.SuperheroId });
    }

    public DbSet<UserFavouriteSuperhero> UserFavouriteSuperheroes { get; set; }
}
