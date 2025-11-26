using ISM.Domain.Identity;

namespace ISM.Application.Abstractions.Services;

public interface ITokenService
{
    TokenPair GenerateTokenPair(ApplicationUser user);
    string ComputeHash(string value);
}

public sealed record TokenPair(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    string RefreshTokenHash
);
