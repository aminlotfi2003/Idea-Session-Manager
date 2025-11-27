namespace ISM.Application.Features.Auth.Dtos;

public sealed record RefreshTokenResponseDto(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt
);
