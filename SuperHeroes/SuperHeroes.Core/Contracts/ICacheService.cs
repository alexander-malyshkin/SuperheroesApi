namespace SuperHeroes.Core.Contracts;

/// <summary>
/// Represents a cache service
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICacheService<T>
{
    /// <summary>
    /// Gets or sets a value in the cache
    /// </summary>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<T?> GetOrSet(string key, Func<string, CancellationToken, Task<T?>> factory, CancellationToken ct);
}
