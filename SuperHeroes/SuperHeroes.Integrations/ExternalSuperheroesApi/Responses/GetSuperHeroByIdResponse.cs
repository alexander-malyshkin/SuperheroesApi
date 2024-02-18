using SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.Responses;

public class GetSuperHeroByIdResponse : SuperHeroApiResponseBase
{
    public SuperHeroDto SuperHero { get; set; }
}
