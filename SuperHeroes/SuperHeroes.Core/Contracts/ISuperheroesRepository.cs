namespace SuperHeroes.Core.Contracts;

public interface ISuperheroesRepository
{
    Task<ICollection<int>> GetFavouritesAsync(int userId, CancellationToken ct);
    Task AddFavouriteAsync(int userId, int superheroId, CancellationToken ct);
}
