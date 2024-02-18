using MediatR;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

public class GetSuperheroesByNameRequest : IRequest<GetSuperheroesByNameResponse>
{
    public string Name { get; set; }
}
