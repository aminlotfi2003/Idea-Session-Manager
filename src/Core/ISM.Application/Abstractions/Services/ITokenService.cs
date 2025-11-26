using ISM.Domain.Identity;
using System.Security.Claims;

namespace ISM.Application.Abstractions.Services;

public interface ITokenService
{
    TokenPair GenerateTokenPair(ApplicationUser user, IEnumerable<string> roles, IEnumerable<Claim>? additionalClaims = null);
    string ComputeHash(string value);
}

public sealed record TokenPair(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    string RefreshTokenHash
);
