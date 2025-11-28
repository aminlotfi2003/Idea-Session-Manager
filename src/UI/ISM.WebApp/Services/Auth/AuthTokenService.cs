using ISM.WebApp.Options;
using ISM.WebApp.Services.Auth.Interfaces;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ISM.WebApp.Services.Auth;

public class AuthTokenService : IAuthTokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtCookieOptions _options;

    public AuthTokenService(IHttpContextAccessor httpContextAccessor, IOptions<JwtCookieOptions> options)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options.Value;
    }

    public string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies[_options.CookieName];
    }

    public void StoreToken(string token, DateTimeOffset expiresAt)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            return;
        }

        context.Response.Cookies.Append(_options.CookieName, token, new CookieOptions
        {
            Path = _options.CookiePath,
            HttpOnly = _options.HttpOnly,
            Secure = _options.Secure,
            SameSite = SameSiteMode.Strict,
            Expires = expiresAt
        });
    }

    public void ClearToken()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            return;
        }

        context.Response.Cookies.Delete(_options.CookieName, new CookieOptions
        {
            Path = _options.CookiePath
        });
    }

    public ClaimsPrincipal GetPrincipalFromToken()
    {
        var token = GetToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwt.Claims, JwtCookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
        catch
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }
}
