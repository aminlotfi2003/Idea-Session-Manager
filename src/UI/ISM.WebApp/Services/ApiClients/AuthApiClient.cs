using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Auth;

namespace ISM.WebApp.Services.ApiClients;

public class AuthApiClient : ApiClientBase, IAuthApiClient
{
    public AuthApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync("auth/login", request, cancellationToken);
        return await ReadAsAsync<AuthResponse>(response);
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsync("auth/logout", null, cancellationToken);
        await EnsureSuccess(response);
    }
}
