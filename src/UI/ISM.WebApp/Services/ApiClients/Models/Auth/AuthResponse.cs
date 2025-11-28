namespace ISM.WebApp.Services.ApiClients.Models.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
}
