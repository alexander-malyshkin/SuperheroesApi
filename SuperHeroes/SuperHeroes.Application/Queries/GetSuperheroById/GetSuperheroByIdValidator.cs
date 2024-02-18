using FluentValidation;

namespace SuperHeroes.Application.Queries.GetSuperheroById;

/// <summary>
/// Represents the validator for the request to get a superhero by its id
/// </summary>
public class GetSuperheroByIdValidator : AbstractValidator<GetSuperheroByIdRequest>
{
    public GetSuperheroByIdValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
