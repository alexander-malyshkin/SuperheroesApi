using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class BiographyDto
{
    [JsonPropertyName("full-name")]
    public string Fullname { get; set; }

    [JsonPropertyName("alter-egos")]
    public string Alteregos { get; set; }
    public ICollection<string> Aliases { get; set; }

    [JsonPropertyName("place-of-birth")]
    public string Placeofbirth { get; set; }

    [JsonPropertyName("first-appearance")]
    public string Firstappearance { get; set; }
    public string Publisher { get; set; }
    public string Alignment { get; set; }
}
