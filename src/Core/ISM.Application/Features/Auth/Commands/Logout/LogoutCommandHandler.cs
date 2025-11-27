using ISM.Application.Common.Abstractions.Repositories.Identity;
using ISM.Application.Common.Abstractions.Services;
using MediatR;

namespace ISM.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IUserRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public LogoutCommandHandler(IUserRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = _tokenService.ComputeHash(request.RefreshToken);
        var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (storedToken is null)
            return;

        storedToken.Revoke();
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
    }
}
