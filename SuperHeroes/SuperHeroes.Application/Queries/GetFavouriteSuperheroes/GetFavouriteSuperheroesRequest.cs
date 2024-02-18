using MediatR;

namespace SuperHeroes.Application.Queries.GetFavouriteSuperheroes;

/// <summary>
/// Represents the request to get the user's favourite superheroes
/// </summary>
public class GetFavouriteSuperheroesRequest : IRequest<GetFavouriteSuperheroesResponse>
{
    public string DummyProp { get; set; }
}
