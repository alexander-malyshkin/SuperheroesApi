using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Commands.MakeSuperheroFavourite;

/// <summary>
/// Represents the response to make a superhero favourite
/// </summary>
public class MakeSuperheroFavouriteResponse : ResponseBase
{
    public MakeSuperheroFavouriteResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
