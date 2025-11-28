using ISM.WebApp.Services.ApiClients.Models.Auth;

namespace ISM.WebApp.Services.Auth.Interfaces;

public interface IAuthenticationService
{
    Task<bool> SignInAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
}
