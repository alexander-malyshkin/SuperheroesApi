using SuperHeroes.Application.Shared;

namespace SuperHeroes.Application.Commands.RemoveFavouriteSuperhero;

public class RemoveFavouriteSuperheroResponse : ResponseBase
{
    public RemoveFavouriteSuperheroResponse(bool success, string? title = null, string? details = null, bool requestValid = true) 
        : base(success, title, details, requestValid)
    {
    }
}
