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
    
    public async Task<ICollection<int>> GetFavouritesAsync(string userToken, CancellationToken ct)
    {
        var userFavourites = await _dbContext.UserFavouriteSuperheroes
            .Where(uf => uf.UserToken == userToken)
            .Select(uf => uf.SuperheroId)
            .ToArrayAsync(ct);
        return userFavourites;
    }
    
    public async Task AddFavouriteAsync(string userToken, int superheroId, CancellationToken ct)
    {
        if (await _dbContext
                .UserFavouriteSuperheroes
                .AnyAsync(_ => _.SuperheroId == superheroId && _.UserToken == userToken, 
                    cancellationToken: ct))
            return;
        
        var userFavourite = new UserFavouriteSuperhero
        {
            UserToken = userToken, 
            SuperheroId = superheroId
        };
        _dbContext.UserFavouriteSuperheroes.Add(userFavourite);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task RemoveFavouriteAsync(string userToken, int superheroId, CancellationToken ct)
    {
        var foundUserFavourite = _dbContext
            .UserFavouriteSuperheroes
            .FirstOrDefault(uf => uf.SuperheroId == superheroId && uf.UserToken == userToken);
        if (foundUserFavourite is null) return;
        
        _dbContext.UserFavouriteSuperheroes.Remove(foundUserFavourite);
        await _dbContext.SaveChangesAsync(ct);
    }
}
