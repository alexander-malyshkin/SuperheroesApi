using System.Text.Json.Serialization;
using SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.Responses;

/// <summary>
/// Represents the response from the superhero search endpoint
/// </summary>
public class SuperheroSearchResponse : SuperHeroApiResponseBase
{
    [JsonPropertyName("results-for")]
    public string Resultsfor { get; set; }

    public ICollection<SuperHeroDto> Results { get; set; }
}
