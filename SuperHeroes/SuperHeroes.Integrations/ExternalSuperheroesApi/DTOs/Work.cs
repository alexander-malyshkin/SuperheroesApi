using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class Work
{
    public string Occupation { get; set; }
    
    [JsonPropertyName("base")]
    public string WorkBase { get; set; }
}
