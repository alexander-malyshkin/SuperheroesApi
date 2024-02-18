namespace SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;

public class SuperHeroDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Powerstats Powerstats { get; set; }
    public BiographyDto Biography { get; set; }
    public AppearanceDto Appearance { get; set; }
    public Work Work { get; set; }
    public ConnectionsDto Connections { get; set; }
    public ImageDto Image { get; set; }
}
