namespace ISM.WebApp.Services.Auth.Interfaces;

public interface IJwtClaimService
{
    bool IsAuthenticated { get; }
    IEnumerable<string> GetRoles();
    string? GetUserName();
}
