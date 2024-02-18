using FluentValidation;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

/// <summary>
/// Represents the validator for the request to make a superhero favourite
/// </summary>
public class MakeSuperheroFavouriteValidator : AbstractValidator<MakeSuperheroFavouriteRequest>
{
    public MakeSuperheroFavouriteValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
