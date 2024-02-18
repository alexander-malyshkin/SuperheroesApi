using FluentValidation;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Exceptions;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

public class MakeSuperheroFavouriteHandler : HandlerBase<MakeSuperheroFavouriteRequest, MakeSuperheroFavouriteResponse>
{
    private readonly ISuperheroesRepository _superheroesRepository;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    
    private const string SuperheroNotFoundMsgFormat = "Superhero with id {0} was not found.";
    
    public MakeSuperheroFavouriteHandler(IValidator<MakeSuperheroFavouriteRequest> validator, 
                                         ISuperheroesRepository superheroesRepository,
                                         IAccessTokenProvider accessTokenProvider,
                                         ISuperheroesExternalProvider superheroesExternalProvider) : base(validator)
    {
        _superheroesRepository = superheroesRepository;
        _accessTokenProvider = accessTokenProvider;
        _superheroesExternalProvider = superheroesExternalProvider;
    }
    
    protected override MakeSuperheroFavouriteResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new MakeSuperheroFavouriteResponse(false, errorTitle, details, isValid);
    
    protected override async Task<MakeSuperheroFavouriteResponse> HandleInternal(MakeSuperheroFavouriteRequest request, CancellationToken ct)
    {
        await EnsureSuperheroExists(request.SuperheroId, ct);
        
        var userToken = _accessTokenProvider.GetToken();
        await _superheroesRepository.AddFavouriteAsync(userToken, request.SuperheroId, ct);

        return new MakeSuperheroFavouriteResponse(true);
    }
    
    private async Task EnsureSuperheroExists(int superheroId, CancellationToken ct)
    {
        var foundSuperhero = await _superheroesExternalProvider.GetById(superheroId, ct);
        if (foundSuperhero is null)
            throw new EntityNotFoundException(nameof(SuperHero), string.Format(SuperheroNotFoundMsgFormat, superheroId));
    }
}
