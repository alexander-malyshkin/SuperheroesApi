namespace SuperHeroes.Api.Endpoints.Shared;

/// <summary>
/// Represents the endpoint routes
/// </summary>
public class EndpointRoutes
{
    public const string GetSuperheroesByName = "/superheroes/search/{name}";
    public const string GetFavouriteSuperheroes = "/superheroes/favourites";
    public const string GetSuperheroesById = "/superheroes/{superheroId}";
    public const string MakeSuperheroFavourite = "/superheroes/{superheroId}";
    public const string RemoveFavouriteSuperhero = "/superheroes/{superheroId}";

}
