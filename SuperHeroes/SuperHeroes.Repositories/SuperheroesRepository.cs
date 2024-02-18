using Microsoft.EntityFrameworkCore;
using SuperHeroes.Core.Contracts;

namespace SuperHeroes.Repositories;

public sealed class SuperheroesRepository : ISuperheroesRepository
{
    private readonly SuperheroesDbContext _dbContext;
    public SuperheroesRepository(SuperheroesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<int>> GetFavouritesAsync(int userId, CancellationToken ct)
    {
        var userFavourites = await _dbContext.UserFavouriteSuperheroes
            .Where(uf => uf.UserId == userId)
            .Select(uf => uf.SuperheroId)
            .ToArrayAsync(ct);
        return userFavourites;
    }
    
    public async Task AddFavouriteAsync(int userId, int superheroId, CancellationToken ct)
    {
        var userFavourite = new UserFavouriteSuperhero
        {
            UserId = userId,
            SuperheroId = superheroId
        };
        _dbContext.UserFavouriteSuperheroes.Add(userFavourite);
        await _dbContext.SaveChangesAsync(ct);
    }
}
