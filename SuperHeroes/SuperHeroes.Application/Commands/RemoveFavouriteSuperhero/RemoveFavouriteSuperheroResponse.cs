using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

/// <summary>
/// Represents the response to remove a superhero from the user's favourites list
/// </summary>
public class RemoveFavouriteSuperheroResponse : ResponseBase
{
    public RemoveFavouriteSuperheroResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
