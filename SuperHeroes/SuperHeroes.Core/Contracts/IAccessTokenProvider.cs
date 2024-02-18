namespace SuperHeroes.Core.Contracts;

/// <summary>
/// Represents the contract for the access token provider
/// </summary>
public interface IAccessTokenProvider
{
    /// <summary>
    /// Gets the token
    /// </summary>
    /// <returns></returns>
    string GetToken();
}
