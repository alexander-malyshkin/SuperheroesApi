using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

public class GetSuperheroByIdResponse : ResponseBase
{
    public SuperheroVm? Superhero { get; init; }

    public GetSuperheroByIdResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
