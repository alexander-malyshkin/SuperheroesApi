using Microsoft.Extensions.Http.Resilience;
using Polly;
using SuperHeroes.Integrations.ExternalSuperheroesApi;

namespace SuperHeroes.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterExternalProviders(this IServiceCollection services,
                                                               IConfiguration config)
    {
        string access_token = config["SuperheroesApi:AccessToken"];
        services
            .AddHttpClient(nameof(SuperheroesExternalProvider), client =>
            {
                client.BaseAddress = new Uri($"https://superheroapi.com/api/{access_token}/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddResilienceHandler(nameof(SuperheroesExternalProvider), builder =>
            {
                builder.AddTimeout(TimeSpan.FromSeconds(30));
                
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 4,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    Delay = TimeSpan.FromSeconds(2)
                });

                builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
                {
                    SamplingDuration = TimeSpan.FromSeconds(5),
                    FailureRatio = 0.9,
                    BreakDuration = TimeSpan.FromSeconds(5)
                });
            });
        
        return services;
    }
}
