namespace ISM.Application.DTOs.Auth;

public sealed record RefreshTokenResponseDto(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt
);
