using MediatR;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

public class GetSuperheroByIdRequest : IRequest<GetSuperheroByIdResponse>
{
    public int SuperheroId { get; set; }
}
