using ISM.WebApp.Services.Auth.Interfaces;
using System.Net.Http.Headers;

namespace ISM.WebApp.Services.Auth;

public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly IAuthTokenService _authTokenService;

    public AuthenticatedHttpClientHandler(IAuthTokenService authTokenService)
    {
        _authTokenService = authTokenService;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _authTokenService.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
