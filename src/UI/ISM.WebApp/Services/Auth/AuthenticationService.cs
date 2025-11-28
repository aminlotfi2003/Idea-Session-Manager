using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Auth;
using ISM.WebApp.Services.Auth.Interfaces;

namespace ISM.WebApp.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthApiClient _authApiClient;
    private readonly IAuthTokenService _authTokenService;

    public AuthenticationService(IAuthApiClient authApiClient, IAuthTokenService authTokenService)
    {
        _authApiClient = authApiClient;
        _authTokenService = authTokenService;
    }

    public async Task<bool> SignInAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _authApiClient.LoginAsync(request, cancellationToken);
        if (result == null || string.IsNullOrWhiteSpace(result.Token))
        {
            return false;
        }

        var expiresAt = result.ExpiresAt == default
            ? DateTimeOffset.UtcNow.AddHours(1)
            : result.ExpiresAt;

        _authTokenService.StoreToken(result.Token, expiresAt);
        return true;
    }

    public async Task SignOutAsync(CancellationToken cancellationToken = default)
    {
        await _authApiClient.LogoutAsync(cancellationToken);
        _authTokenService.ClearToken();
    }
}
