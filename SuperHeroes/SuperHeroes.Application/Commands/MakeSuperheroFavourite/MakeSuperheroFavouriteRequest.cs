using MediatR;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

public class MakeSuperheroFavouriteRequest : IRequest<MakeSuperheroFavouriteResponse>
{
    public int SuperheroId { get; set; }
}
