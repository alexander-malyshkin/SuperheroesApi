using FluentValidation;
using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Exceptions;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

public class GetSuperheroByIdQuery : HandlerBase<GetSuperheroByIdRequest, GetSuperheroByIdResponse>
{
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    private readonly ISuperheroesRepository _superheroesRepository;
    private readonly IAccessTokenProvider _accessTokenProvider;
    
    private const string SuperheroNotFoundMsgFormat = "Superhero with id {0} was not found.";
    
    public GetSuperheroByIdQuery(IValidator<GetSuperheroByIdRequest> validator, 
                                 ISuperheroesExternalProvider superheroesExternalProvider,
                                 ISuperheroesRepository superheroesRepository,
                                 IAccessTokenProvider accessTokenProvider) 
        : base(validator)
    {
        _superheroesExternalProvider = superheroesExternalProvider;
        _superheroesRepository = superheroesRepository;
        _accessTokenProvider = accessTokenProvider;
    }
    
    protected override GetSuperheroByIdResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new GetSuperheroByIdResponse(null, false, errorTitle, details, isValid);
    
    protected override async Task<GetSuperheroByIdResponse> HandleInternal(GetSuperheroByIdRequest request, CancellationToken ct)
    {
        var foundSuperhero = await _superheroesExternalProvider.GetById(request.SuperheroId, ct);
        if (foundSuperhero is null)
            throw new EntityNotFoundException(nameof(SuperHero), string.Format(SuperheroNotFoundMsgFormat, request.SuperheroId));
        
        var userToken = _accessTokenProvider.GetToken();
        var userFavouriteSuperheroes = await _superheroesRepository.GetFavouritesAsync(userToken, ct);

        SuperheroVm superhero = ConvertSuperheroToVm(foundSuperhero, userFavouriteSuperheroes);
        return new GetSuperheroByIdResponse(superhero, true);
    }
    private SuperheroVm ConvertSuperheroToVm(SuperHero superHero, ICollection<int> userFavouriteSuperheroes)
    {
        bool isFavourite = userFavouriteSuperheroes.Contains(superHero.Id);
        return new SuperheroVm(superHero.Id, superHero.Name, isFavourite);
    }

}
