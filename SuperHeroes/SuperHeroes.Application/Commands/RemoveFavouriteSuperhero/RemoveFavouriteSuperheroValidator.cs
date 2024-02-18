using FluentValidation;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

/// <summary>
/// Represents the validator for the request to remove a superhero from the user's favourites list
/// </summary>
public class RemoveFavouriteSuperheroValidator : AbstractValidator<RemoveFavouriteSuperheroRequest>
{
    public RemoveFavouriteSuperheroValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
