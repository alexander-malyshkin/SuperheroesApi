using FluentValidation;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Models;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

public class GetSuperheroesByNameQuery : HandlerBase<GetSuperheroesByNameRequest, GetSuperheroesByNameResponse>
{
    private readonly ISuperheroesExternalProvider _superheroesExternalProvider;
    public GetSuperheroesByNameQuery(IValidator<GetSuperheroesByNameRequest> validator, ISuperheroesExternalProvider superheroesExternalProvider) : base(validator)
    {
        _superheroesExternalProvider = superheroesExternalProvider;
    }
    protected override GetSuperheroesByNameResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid) =>
        new GetSuperheroesByNameResponse(Array.Empty<SuperheroVm>(), false, errorTitle, details, isValid);
    
    protected override async Task<GetSuperheroesByNameResponse> HandleInternal(GetSuperheroesByNameRequest request, CancellationToken ct)
    {
        ICollection<SuperHero> superheroes = await _superheroesExternalProvider.SearchByNameAsync(request.Name, ct);
        ICollection<SuperheroVm> superheroesVm = superheroes.Select(x => new SuperheroVm(x.Id, x.Name)).ToArray();
        return new GetSuperheroesByNameResponse(superheroesVm, true);
    }
}
