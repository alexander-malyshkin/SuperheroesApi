using System.Text.Json.Serialization;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class AppearanceDto
{
    public string Gender { get; set; }
    public string Race { get; set; }
    public ICollection<string> Height { get; set; }
    public ICollection<string> Weight { get; set; }

    [JsonPropertyName("eye-color")]
    public string Eyecolor { get; set; }

    [JsonPropertyName("hair-color")]
    public string Haircolor { get; set; }
}
