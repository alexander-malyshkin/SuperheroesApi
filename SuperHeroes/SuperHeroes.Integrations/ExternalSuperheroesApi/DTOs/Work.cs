using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

/// <summary>
/// Represents the work of a superhero
/// </summary>
public class Work
{
    public string Occupation { get; set; }
    
    [JsonPropertyName("base")]
    public string WorkBase { get; set; }
}
