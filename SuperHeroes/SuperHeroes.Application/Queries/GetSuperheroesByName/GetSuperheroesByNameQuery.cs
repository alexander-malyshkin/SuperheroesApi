using FluentValidation;
using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

public class GetSuperheroesByNameQuery : HandlerBase<GetSuperheroesByNameRequest, GetSuperheroesByNameResponse>
{
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly ISuperheroesRepository _superheroesRepository;
    public GetSuperheroesByNameQuery(IValidator<GetSuperheroesByNameRequest> validator, 
                                     ISuperheroesExternalProvider superheroesExternalProvider, 
                                     IAccessTokenProvider accessTokenProvider,
                                     ISuperheroesRepository superheroesRepository) : base(validator)
    {
        _superheroesExternalProvider = superheroesExternalProvider;
        _accessTokenProvider = accessTokenProvider;
        _superheroesRepository = superheroesRepository;
    }
    protected override GetSuperheroesByNameResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new GetSuperheroesByNameResponse(false, errorTitle, details, isValid);
    
    protected override async Task<GetSuperheroesByNameResponse> HandleInternal(GetSuperheroesByNameRequest request, CancellationToken ct)
    {
        ICollection<SuperHero> superheroes = await _superheroesExternalProvider.SearchByNameAsync(request.Name, ct);
        if (!superheroes.Any())
            return new GetSuperheroesByNameResponse( true)
            {
                Superheroes = Array.Empty<SuperheroVm>()
            };

        var userToken = _accessTokenProvider.GetToken();
        var userFavouriteSuperheroes = await _superheroesRepository.GetFavouritesAsync(userToken, ct);
        
        var superheroesVms = ConvertToVms(superheroes, userFavouriteSuperheroes).ToArray();
        return new GetSuperheroesByNameResponse(true)
        {
            Superheroes = superheroesVms
        };
    }
    private IEnumerable<SuperheroVm> ConvertToVms(ICollection<SuperHero> superheroes, ICollection<int> userFavouriteSuperheroes)
    {
        var favouritesSet = userFavouriteSuperheroes.ToHashSet();
        foreach (SuperHero superHero in superheroes)
        {
            yield return new SuperheroVm(superHero.Id, superHero.Name, favouritesSet.Contains(superHero.Id));
        }
    }
}
