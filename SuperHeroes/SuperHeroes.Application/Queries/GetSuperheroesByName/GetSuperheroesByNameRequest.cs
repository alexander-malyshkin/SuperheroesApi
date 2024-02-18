using MediatR;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

/// <summary>
/// Represents the request to get superheroes by their name
/// </summary>
public class GetSuperheroesByNameRequest : IRequest<GetSuperheroesByNameResponse>
{
    public string Name { get; set; }
}
