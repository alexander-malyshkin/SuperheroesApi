namespace SuperHeroes.Api.Endpoints.Shared;

public class EndpointRoutes
{
    public const string GetSuperheroesByName = "/superheroes/search/{name}";
    public const string GetSuperheroesById = "/superheroes/{superheroId}";
    public const string MakeSuperheroFavourite = "/superheroes/{superheroId}";

}
