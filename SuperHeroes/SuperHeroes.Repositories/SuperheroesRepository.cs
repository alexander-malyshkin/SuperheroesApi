using SuperHeroes.Core.Contracts;

namespace SuperHeroes.Repositories;

public sealed class SuperheroesRepository : ISuperheroesRepository
{

    public Task<ICollection<int>> GetFavouritesAsync(int userId, CancellationToken ct) =>
        throw new NotImplementedException();
    public Task AddFavouriteAsync(int userId, int superheroId, CancellationToken ct) =>
        throw new NotImplementedException();
}
