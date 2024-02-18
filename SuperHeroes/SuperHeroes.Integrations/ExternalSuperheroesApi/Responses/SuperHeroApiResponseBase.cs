namespace SuperHeroes.Integrations.ExternalSuperheroesApi.Responses;

public abstract class SuperHeroApiResponseBase
{
    private const string SuccessString = "success";
    public string Response { get; set; }
    public string Error { get; set; }
    public bool Success => Response == SuccessString;
}
