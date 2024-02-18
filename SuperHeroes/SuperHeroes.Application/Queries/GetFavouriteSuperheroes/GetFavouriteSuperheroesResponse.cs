using SuperHeroes.Application.Queries.ViewModels;
using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Queries.GetFavouriteSuperheroes;

public class GetFavouriteSuperheroesResponse : ResponseBase
{
    public ICollection<SuperheroVm> Superheroes { get; init; }

    public GetFavouriteSuperheroesResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
