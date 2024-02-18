namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class SuperHeroDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Powerstats Powerstats { get; set; }
    public BiographyDto BiographyDto { get; set; }
    public AppearanceDto AppearanceDto { get; set; }
    public Work Work { get; set; }
    public ConnectionsDto ConnectionsDto { get; set; }
    public ImageDto ImageDto { get; set; }
}
