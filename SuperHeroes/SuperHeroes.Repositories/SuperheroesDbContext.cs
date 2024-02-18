using Microsoft.EntityFrameworkCore;

namespace SuperHeroes.Repositories;

public class SuperheroesDbContext : DbContext
{
    public SuperheroesDbContext(DbContextOptions<SuperheroesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFavouriteSuperhero>()
            .HasKey(uf => new { uf.UserToken, uf.SuperheroId });
    }

    public DbSet<UserFavouriteSuperhero> UserFavouriteSuperheroes { get; set; }
}
