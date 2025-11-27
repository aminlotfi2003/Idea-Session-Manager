using ISM.Application.Common.Abstractions.Repositories.Identity;
using ISM.Application.Common.Abstractions.Services;
using ISM.Application.Features.Auth.Dtos;
using ISM.Domain.Identity;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ISM.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
{
    private readonly IUserRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _clock;

    public RefreshTokenCommandHandler(
        IUserRefreshTokenRepository refreshTokenRepository,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IDateTimeProvider clock)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _tokenService = tokenService;
        _clock = clock;
    }

    public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = _tokenService.ComputeHash(request.RefreshToken);
        var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (storedToken is null)
            throw new UnauthorizedException("Refresh token not found.");

        if (!storedToken.IsActive(_clock.UtcNow))
            throw new UnauthorizedException("Refresh token is no longer valid.");

        storedToken.Revoke();

        var user = storedToken.User;
        var roles = await _userManager.GetRolesAsync(user);
        var tokenPair = _tokenService.GenerateTokenPair(user, roles, Enumerable.Empty<Claim>());

        var newRefreshToken = new UserRefreshToken(user.Id, tokenPair.RefreshTokenHash, tokenPair.RefreshTokenExpiresAt);
        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResponseDto(
            tokenPair.AccessToken,
            tokenPair.AccessTokenExpiresAt,
            tokenPair.RefreshToken,
            tokenPair.RefreshTokenExpiresAt);
    }
}
