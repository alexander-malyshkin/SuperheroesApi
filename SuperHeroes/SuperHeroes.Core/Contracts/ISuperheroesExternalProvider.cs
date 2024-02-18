using SuperHeroes.Core.Models;

namespace SuperHeroes.Core.Contracts;

/// <summary>
/// Represents an external provider for superheroes
/// </summary>
public interface ISuperheroesExternalProvider
{
    /// <summary>
    /// Searches for superheroes by name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<ICollection<SuperHero>> SearchByNameAsync(string name, CancellationToken ct);
    
    /// <summary>
    /// Gets a superhero by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<SuperHero?> GetById(int id, CancellationToken ct);
}
