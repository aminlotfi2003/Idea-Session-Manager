using ISM.WebApp.Services.ApiClients.Models.Auth;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IAuthApiClient
{
    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
}
