using System.Text.Json.Serialization;
using SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi;

public class SuperheroSearchResponse
{
    public string Response { get; set; }

    [JsonPropertyName("results-for")]
    public string Resultsfor { get; set; }

    public ICollection<SuperHeroDto> Results { get; set; }
}
