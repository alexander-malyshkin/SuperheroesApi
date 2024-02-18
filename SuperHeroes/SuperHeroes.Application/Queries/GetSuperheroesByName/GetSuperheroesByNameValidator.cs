using FluentValidation;

namespace SuperHeroes.Application.Queries.GetSuperheroesByName;

public class GetSuperheroesByNameValidator : AbstractValidator<GetSuperheroesByNameRequest>
{
    public GetSuperheroesByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}
