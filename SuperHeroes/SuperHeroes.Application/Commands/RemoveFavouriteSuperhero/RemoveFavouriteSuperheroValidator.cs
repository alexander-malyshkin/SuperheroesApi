using FluentValidation;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

public class RemoveFavouriteSuperheroValidator : AbstractValidator<RemoveFavouriteSuperheroRequest>
{
    public RemoveFavouriteSuperheroValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
