using FluentValidation;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

/// <summary>
/// Represents the validator for the request to get superheroes by their name
/// </summary>
public class GetSuperheroesByNameValidator : AbstractValidator<GetSuperheroesByNameRequest>
{
    public GetSuperheroesByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}
