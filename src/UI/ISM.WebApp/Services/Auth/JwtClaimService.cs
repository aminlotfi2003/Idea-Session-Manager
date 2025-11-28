using ISM.WebApp.Services.Auth.Interfaces;
using System.Security.Claims;

namespace ISM.WebApp.Services.Auth;

public class JwtClaimService : IJwtClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> GetRoles()
    {
        var claims = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role) ?? Enumerable.Empty<Claim>();
        return claims.Select(c => c.Value);
    }

    public string? GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ??
               _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}
