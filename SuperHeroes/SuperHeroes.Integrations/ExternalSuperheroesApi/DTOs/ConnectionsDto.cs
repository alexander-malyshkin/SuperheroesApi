using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class ConnectionsDto
{
    [JsonPropertyName("group-affiliation")]
    public string Groupaffiliation { get; set; }
    public string Relatives { get; set; }
}
