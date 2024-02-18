using FluentValidation;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Exceptions;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

public class RemoveFavouriteSuperheroHandler : HandlerBase<RemoveFavouriteSuperheroRequest, RemoveFavouriteSuperheroResponse>
{
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    private readonly ISuperheroesRepository _superheroesRepository;
    private readonly IAccessTokenProvider _accessTokenProvider;
    
    public RemoveFavouriteSuperheroHandler(IValidator<RemoveFavouriteSuperheroRequest> validator, 
                                           ISuperheroesExternalProvider superheroesExternalProvider, 
                                           ISuperheroesRepository superheroesRepository, 
                                           IAccessTokenProvider accessTokenProvider) 
        : base(validator)
    {
        _superheroesExternalProvider = superheroesExternalProvider;
        _superheroesRepository = superheroesRepository;
        _accessTokenProvider = accessTokenProvider;
    }
    
    protected override RemoveFavouriteSuperheroResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new RemoveFavouriteSuperheroResponse(false, errorTitle, details, isValid);
    
    protected override async Task<RemoveFavouriteSuperheroResponse> HandleInternal(RemoveFavouriteSuperheroRequest request, CancellationToken ct)
    {
        await EnsureSuperheroExists(request.SuperheroId, ct);
        
        var userToken = _accessTokenProvider.GetToken();
        await _superheroesRepository.RemoveFavouriteAsync(userToken, request.SuperheroId, ct);
        
        return new RemoveFavouriteSuperheroResponse(true);
    }
    private async Task EnsureSuperheroExists(int superheroId, CancellationToken ct)
    {
        SuperHero? superHero = await _superheroesExternalProvider.GetById(superheroId, ct);
        if (superHero is null)
            throw new EntityNotFoundException(nameof(SuperHero), $"Superhero with id {superheroId} does not exist");
        
    }
}
