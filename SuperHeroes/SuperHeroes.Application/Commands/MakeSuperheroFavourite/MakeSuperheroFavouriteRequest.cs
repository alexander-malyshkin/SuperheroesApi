using MediatR;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

/// <summary>
/// Represents the request to make a superhero favourite
/// </summary>
public class MakeSuperheroFavouriteRequest : IRequest<MakeSuperheroFavouriteResponse>
{
    public int SuperheroId { get; set; }
}
