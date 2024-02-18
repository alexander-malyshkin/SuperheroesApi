using SuperHeroes.Core.Contracts;
using SuperHeroes.Integrations.ExternalSuperheroesApi;

namespace SuperHeroes.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterExternalProviders(this IServiceCollection services,
                                                               IConfiguration config)
    {
        services.AddScoped<IAccessTokenProvider, HttpRequestAccessTokenProvider>();
        
        services
            .AddHttpClient(nameof(SuperheroesExternalProvider), client =>
            {
                client.BaseAddress = new Uri($"https://superheroapi.com/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddStandardResilienceHandler();
        
        return services;
    }
}
