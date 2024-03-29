using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

/// <summary>
/// Represents the response to get superheroes by their name
/// </summary>
public class GetSuperheroesByNameResponse : ResponseBase
{
    public ICollection<SuperheroVm> Superheroes { get; init; }

    public GetSuperheroesByNameResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
