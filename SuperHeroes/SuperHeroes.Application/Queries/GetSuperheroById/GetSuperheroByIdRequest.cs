using MediatR;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

/// <summary>
/// Represents the request to get a superhero by its id
/// </summary>
public class GetSuperheroByIdRequest : IRequest<GetSuperheroByIdResponse>
{
    public int SuperheroId { get; set; }
}
