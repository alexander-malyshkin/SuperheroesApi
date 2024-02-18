using FluentValidation;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

public class MakeSuperheroFavouriteValidator : AbstractValidator<MakeSuperheroFavouriteRequest>
{
    public MakeSuperheroFavouriteValidator()
    {
        RuleFor(x => x.SuperheroId).GreaterThan(0);
    }
}
