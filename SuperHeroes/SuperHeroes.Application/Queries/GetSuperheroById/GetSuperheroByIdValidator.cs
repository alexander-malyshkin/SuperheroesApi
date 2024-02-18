using FluentValidation;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

public class GetSuperheroByIdValidator : AbstractValidator<GetSuperheroByIdRequest>
{
    public GetSuperheroByIdValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
