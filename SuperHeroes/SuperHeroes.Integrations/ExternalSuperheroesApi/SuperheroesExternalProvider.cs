using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Core.Exceptions;
using SuperHeroes.Core.Models;
using SuperHeroes.Integrations.ExternalSuperheroesApi.DTOs;
using SuperHeroes.Integrations.ExternalSuperheroesApi.Responses;

namespace SuperHeroes.Integrations.ExternalSuperheroesApi;

public sealed class SuperheroesExternalProvider : ISuperheroesExternalProvider, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SuperheroesExternalProvider> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    public SuperheroesExternalProvider(IHttpClientFactory factory, ILogger<SuperheroesExternalProvider> logger, JsonSerializerOptions jsonSerializerOptions)
    {
        _logger = logger;
        _jsonSerializerOptions = jsonSerializerOptions;
        _httpClient = factory.CreateClient(nameof(SuperheroesExternalProvider));
    }
    public async Task<ICollection<SuperHero>> SearchByNameAsync(string name, CancellationToken ct)
    {
        var relativeSearchPath = $"search/{name}";
        var foundSuperheroes = await GetFromNetwork<SuperheroSearchResponse>(relativeSearchPath, ct);
        if (foundSuperheroes is null)
            return Array.Empty<SuperHero>();

        return GetValidSuperHeroes(foundSuperheroes.Results).ToArray();
    }
    
    private IEnumerable<SuperHero> GetValidSuperHeroes(ICollection<SuperHeroDto> superHeroDtos)
    {
        foreach (SuperHeroDto superHeroDto in superHeroDtos)
        {
            if (!int.TryParse(superHeroDto.Id, out var parsedId))
                continue;

            yield return new SuperHero
            {
                Id = parsedId,
                Name = superHeroDto.Name
            };
        }
    }

    public async Task<SuperHero?> GetById(int id, CancellationToken ct)
    {
        var relativePath = $"{id}";
        var superHeroResponse = await GetFromNetwork<GetSuperHeroByIdResponse>(relativePath, ct);
        if (superHeroResponse is null)
            return null;
        
        string heroStringId = superHeroResponse.SuperHero.Id;
        return int.TryParse(heroStringId, out var parsedId)
            ? new SuperHero
            {
                Id = parsedId,
                Name = superHeroResponse.SuperHero.Name
            }
            : null;
    }

    private async Task<T?> GetFromNetwork<T>(string relativePath, CancellationToken ct)
        where T : SuperHeroApiResponseBase
    {
        HttpResponseMessage response;
        string contentString;

        try
        {
            response = await _httpClient.GetAsync(relativePath, ct);
            contentString = await response.Content.ReadAsStringAsync(ct);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while fetching data from the network");
            throw new IntegrationReadException(e.Message);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return default(T?);
        }
        else if (response.StatusCode >= HttpStatusCode.InternalServerError)
        {
            throw new IntegrationReadException(nameof(HttpStatusCode.InternalServerError));
        }

        var deserializedResponse = JsonSerializer.Deserialize<T>(contentString, _jsonSerializerOptions);
        if (deserializedResponse is null || !deserializedResponse.Success)
            throw new IntegrationReadException("The response was not successful.");
        
        return deserializedResponse;
    }
    
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
