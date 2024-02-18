namespace SuperHeroes.Core.Contracts;

/// <summary>
/// Represents a superheroes repository
/// </summary>
public interface ISuperheroesRepository
{
    /// <summary>
    /// Gets the favourite superheroes for a user
    /// </summary>
    /// <param name="userToken"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<ICollection<int>> GetFavouritesAsync(string userToken, CancellationToken ct);
    
    /// <summary>
    /// Adds a superhero to the user's favourites
    /// </summary>
    /// <param name="userToken"></param>
    /// <param name="superheroId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task AddFavouriteAsync(string userToken, int superheroId, CancellationToken ct);
    
    /// <summary>
    /// Removes a superhero from the user's favourites
    /// </summary>
    /// <param name="userToken"></param>
    /// <param name="superheroId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task RemoveFavouriteAsync(string userToken, int superheroId, CancellationToken ct);
}
