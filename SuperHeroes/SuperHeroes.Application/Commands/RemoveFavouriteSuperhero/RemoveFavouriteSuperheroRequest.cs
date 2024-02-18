using MediatR;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

/// <summary>
/// Represents the request to remove a superhero from the user's favourites list
/// </summary>
public class RemoveFavouriteSuperheroRequest : IRequest<RemoveFavouriteSuperheroResponse>
{
    public int SuperheroId { get; set; }
}
