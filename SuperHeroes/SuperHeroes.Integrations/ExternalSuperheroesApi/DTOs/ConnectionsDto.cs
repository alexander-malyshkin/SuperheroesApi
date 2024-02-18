using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

/// <summary>
/// Represents the appearance of a superhero
/// </summary>
public class ConnectionsDto
{
    [JsonPropertyName("group-affiliation")]
    public string Groupaffiliation { get; set; }
    public string Relatives { get; set; }
}
