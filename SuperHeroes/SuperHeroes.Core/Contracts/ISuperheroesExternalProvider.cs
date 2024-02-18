using SuperHeroes.Core.Models;

namespace SuperHeroes.Core.Contracts;

public interface ISuperheroesExternalProvider
{
    Task<ICollection<SuperHero>> SearchByNameAsync(string name, CancellationToken ct);
    Task<SuperHero?> GetById(int id, CancellationToken ct);
}
