using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Integrations.ExternalSuperheroesApi;
using SuperHeroes.Repositories;

namespace SuperHeroes.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterExternalProviders(this IServiceCollection services,
                                                               IConfiguration config)
    {
        services.AddScoped<IAccessTokenProvider, HttpRequestAccessTokenProvider>();
        services.AddScoped<ISuperheroesExternalProvider, SuperheroesExternalProvider>();
        services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        services
            .AddHttpClient(nameof(SuperheroesExternalProvider), client =>
            {
                client.BaseAddress = new Uri($"https://superheroapi.com/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddStandardResilienceHandler();
        
        return services;
    }
    
    public static IServiceCollection RegisterDalServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<SuperheroesDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("SuperheroesDb"));
        });
        services.AddScoped<ISuperheroesRepository, SuperheroesRepository>();
        return services;
    }
}
