namespace ISM.Application.DTOs.Auth;

public sealed record LoginResponseDto(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    bool MustChangePassword
);
