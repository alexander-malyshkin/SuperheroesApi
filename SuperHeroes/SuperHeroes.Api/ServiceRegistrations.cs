using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using SuperHeroes.Core.Contracts;
using SuperHeroes.Integrations.ExternalSuperheroesApi;
using SuperHeroes.Integrations.Redis;
using SuperHeroes.Repositories;

namespace SuperHeroes.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterExternalProviders(this IServiceCollection services,
                                                               IConfiguration config)
    {
        services.AddScoped<IAccessTokenProvider, HttpRequestAccessTokenProvider>();
        services.RegisterSuperheroesProvider();
        services.AddSingleton(typeof(ICacheService<>), typeof(RedisService<>));
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

    public static IServiceCollection AddRedisCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            string redisOptions = configuration.GetValue<string>("Redis")
                ?? throw new ApplicationException("Redis connection string not found");
            return ConnectionMultiplexer.Connect(redisOptions);
        });

        services.AddSingleton(typeof(ICacheService<>), typeof(RedisService<>));
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
    
    private static void RegisterSuperheroesProvider(this IServiceCollection services)
    {
        services.RegisterDecorator<ISuperheroesExternalProvider, SuperheroesExternalProvider, CachedSuperheroesExternalProvider>
            (SuperheroesExternalProviderConstants.PlainSuperheroesProviderKey);
    }
    
    public static IServiceCollection RegisterDecorator<TContract, TUnderlying, TDecorator>(this IServiceCollection services, string key) 
        where TContract : class
        where TUnderlying : class, TContract
        where TDecorator : class, TContract
    {
        return services
            .AddKeyedScoped<TContract, TUnderlying>(key)
            .AddScoped<TContract, TDecorator>();
    }
}
