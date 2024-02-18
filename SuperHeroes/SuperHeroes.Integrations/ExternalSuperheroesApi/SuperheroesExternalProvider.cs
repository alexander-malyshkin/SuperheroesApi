using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi;

public sealed class SuperheroesExternalProvider : ISuperheroesExternalProvider
{

    public Task<ICollection<SuperHero>> SearchByNameAsync(string name, CancellationToken ct) =>
        throw new NotImplementedException();
    public Task<SuperHero?> GetById(int id, CancellationToken ct) =>
        throw new NotImplementedException();
}
