using MediatR;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

public class RemoveFavouriteSuperheroRequest : IRequest<RemoveFavouriteSuperheroResponse>
{
    public int SuperheroId { get; set; }
}
