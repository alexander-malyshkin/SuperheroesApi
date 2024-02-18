using Microsoft.Extensions.DependencyInjection;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi;

/// <summary>
/// Represents a cached version of the superheroes external provider (utilizing the Decorator pattern)
/// </summary>
public sealed class CachedSuperheroesExternalProvider : ISuperheroesExternalProvider
{
    private readonly ISuperheroesExternalProvider _externalProvider;
    private readonly ICacheService<SuperHero> _singleSuperheroCacheService;
    private readonly ICacheService<ICollection<SuperHero>> _multipleSuperheroesCacheBulkService;
    
    public CachedSuperheroesExternalProvider([FromKeyedServices(SuperheroesExternalProviderConstants.PlainSuperheroesProviderKey)] ISuperheroesExternalProvider externalProvider, 
                                             ICacheService<SuperHero> singleSuperheroCacheService,
                                             ICacheService<ICollection<SuperHero>> multipleSuperheroesCacheBulkService)
    {
        _externalProvider = externalProvider;
        _singleSuperheroCacheService = singleSuperheroCacheService;
        _multipleSuperheroesCacheBulkService = multipleSuperheroesCacheBulkService;
    }


    /// <inheritdoc cref="ISuperheroesExternalProvider.SearchByNameAsync"/>
    public Task<ICollection<SuperHero>> SearchByNameAsync(string name, CancellationToken ct)
    {
        return _multipleSuperheroesCacheBulkService.GetOrSet(name, 
            async (key, ctToken) =>  await _externalProvider.SearchByNameAsync(key, ctToken), 
            ct)!;
    }
    
    /// <inheritdoc cref="ISuperheroesExternalProvider.GetById"/>
    public Task<SuperHero?> GetById(int id, CancellationToken ct)
    {
        return _singleSuperheroCacheService.GetOrSet(id.ToString(), 
            async (key, ctToken) =>  await _externalProvider.GetById(int.Parse(key), ctToken), 
            ct);
    }
}
