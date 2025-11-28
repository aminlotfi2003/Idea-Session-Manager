using System.Security.Claims;

namespace ISM.WebApp.Services.Auth.Interfaces;

public interface IAuthTokenService
{
    string? GetToken();
    void StoreToken(string token, DateTimeOffset expiresAt);
    void ClearToken();
    ClaimsPrincipal GetPrincipalFromToken();
}
