using MediatR;

namespace SuperHeroes.Application.Queries.GetFavouriteSuperheroes;

public class GetFavouriteSuperheroesRequest : IRequest<GetFavouriteSuperheroesResponse>
{
    public string DummyProp { get; set; }
}
