using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

public class GetSuperheroesByNameResponse : ResponseBase
{
    public ICollection<SuperheroVm> Superheroes { get; }

    public GetSuperheroesByNameResponse(ICollection<SuperheroVm> superheroes, bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
        Superheroes = superheroes;
    }
}
