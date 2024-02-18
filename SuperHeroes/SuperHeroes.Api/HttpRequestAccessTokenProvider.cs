using SuperHeroes.Core.Contracts;

namespace SuperHeroes.Api;

/// <summary>
/// Allows to get the access token rom the HTTP request.
/// Should be registered as a scoped service.
/// </summary>
public class HttpRequestAccessTokenProvider : IAccessTokenProvider
{
    private readonly HttpContent _httpContent;
    private const string AccessTokenHeader = "Facebook-Access-Token";
    
    public HttpRequestAccessTokenProvider(HttpContent httpContent)
    {
        _httpContent = httpContent;
    }

    public string GetToken()
    {
        return _httpContent.Headers.GetValues(AccessTokenHeader).FirstOrDefault()
            ?? throw new ApplicationException("Access token not found in the request.");
    }
}
