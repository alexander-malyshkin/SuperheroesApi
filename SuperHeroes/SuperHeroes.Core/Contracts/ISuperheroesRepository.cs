namespace SuperHeroes.Core.Contracts;

public interface ISuperheroesRepository
{
    Task<ICollection<int>> GetFavouritesAsync(string userToken, CancellationToken ct);
    Task AddFavouriteAsync(string userToken, int superheroId, CancellationToken ct);
    Task RemoveFavouriteAsync(string userToken, int superheroId, CancellationToken ct);
}
