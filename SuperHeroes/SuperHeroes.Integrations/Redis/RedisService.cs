using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using SuperHeroes.Core.Contracts;

namespace SuperHeroes.Integrations.Redis;

public sealed class RedisService<T> : ICacheService<T>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly ILogger<RedisService<T>> _logger;
    private readonly TimeSpan? _cacheExpiry;
    private readonly IDatabase _redisDb;

    public RedisService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration, JsonSerializerOptions jsonSerializerOptions,
                        ILogger<RedisService<T>> logger)
    {
        _redisDb = connectionMultiplexer.GetDatabase();
        _jsonSerializerOptions = jsonSerializerOptions;
        _logger = logger;
        var cacheExpirySeconds = int.TryParse(configuration["CacheExpirySeconds"], out int resultInt)
            ? resultInt
            : int.MaxValue;
        
        _cacheExpiry = TimeSpan.FromSeconds(cacheExpirySeconds);
    }

    public async Task<T?> GetOrSet(string key, Func<string, CancellationToken, Task<T?>> factory, CancellationToken ct)
    {
        T? foundValue = await Get(key);
        if (foundValue != null)
            return foundValue;
        
        ArgumentNullException.ThrowIfNull(factory);

        T? value;
        try
        {
            value = await factory.Invoke(key, ct);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get value for populating cache");
            throw;
        }

        try
        {
            await SetWhenDoesNotExist(key, value);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to set value in cache");
            throw;
        }
        
        return await Get(key);
    }


    private async ValueTask<T?> Get(string key)
    {
        RedisValue stringVal = await _redisDb.StringGetAsync(key);
        
        return stringVal != default  
                ? JsonSerializer.Deserialize<T>(stringVal!, _jsonSerializerOptions)
                : default(T?);
    }
    
    private async ValueTask SetWhenDoesNotExist(string key, T? value)
    {
        RedisValue stringValue = JsonSerializer.Serialize(value, _jsonSerializerOptions);
        await _redisDb.StringSetAsync(key, stringValue, when: When.NotExists, expiry: _cacheExpiry);
    }
}
