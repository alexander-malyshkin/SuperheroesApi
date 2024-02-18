using FluentValidation;
using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Queries.GetFavouriteSuperheroes;

public class GetFavouriteSuperheroesQuery : HandlerBase<GetFavouriteSuperheroesRequest, GetFavouriteSuperheroesResponse>
{
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    private readonly ISuperheroesRepository _superheroesRepository;
    private readonly IAccessTokenProvider _accessTokenProvider;
    
    public GetFavouriteSuperheroesQuery(IValidator<GetFavouriteSuperheroesRequest> validator, 
                                        ISuperheroesExternalProvider superheroesExternalProvider,
                                        ISuperheroesRepository superheroesRepository,
                                        IAccessTokenProvider accessTokenProvider) 
        : base(validator)
    {
        _superheroesExternalProvider = superheroesExternalProvider;
        _superheroesRepository = superheroesRepository;
        _accessTokenProvider = accessTokenProvider;
    }

    protected override GetFavouriteSuperheroesResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new GetFavouriteSuperheroesResponse(false, errorTitle, details, isValid);
    
    protected override async Task<GetFavouriteSuperheroesResponse> HandleInternal(GetFavouriteSuperheroesRequest request, CancellationToken ct)
    {
        var userToken = _accessTokenProvider.GetToken();
        var favouriteSuperheroeIds = await _superheroesRepository.GetFavouritesAsync(userToken, ct);

        var superheroesVms = new List<SuperheroVm>(favouriteSuperheroeIds.Count);
        await foreach(SuperHero superHero in LookupAllSuperheroes(favouriteSuperheroeIds, ct))
        {
            superheroesVms.Add(new SuperheroVm(superHero.Id, superHero.Name, true));
        }
        
        return new GetFavouriteSuperheroesResponse(true)
        {
            Superheroes = superheroesVms
        };
    }
    
    private async IAsyncEnumerable<SuperHero> LookupAllSuperheroes(ICollection<int> favouriteSuperheroeIds, CancellationToken ct)
    {
        foreach (int superheroId in favouriteSuperheroeIds)
        {
            SuperHero? superHero = await _superheroesExternalProvider.GetById(superheroId, ct);
            if (superHero is null) continue;
            
            yield return superHero;
        }
    }
}
