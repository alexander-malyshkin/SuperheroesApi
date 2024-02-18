using Microsoft.Extensions.Primitives;
using SuperHeroes.Core.Contracts;

namespace SuperHeroes.Api;

/// <summary>
/// Allows to get the access token rom the HTTP request.
/// Should be registered as a scoped service.
/// </summary>
public class HttpRequestAccessTokenProvider : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string AccessTokenHeader = "Facebook-Access-Token";
    
    public HttpRequestAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetToken()
    {
        return (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(AccessTokenHeader, out StringValues token)
            ? token.FirstOrDefault()
            : throw new ApplicationException("Access token not found in the request."))!;
    }
}
