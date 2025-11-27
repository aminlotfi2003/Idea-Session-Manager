using ISM.Application.Abstractions.Persistence;
using ISM.Application.Abstractions.Repositories.Identity;
using ISM.Application.Abstractions.Services;
using ISM.Application.DTOs.Auth;
using ISM.Domain.Identity;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ISM.Application.Queries.Auth.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUserRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserLoginHistoryRepository _loginHistoryRepository;
    private readonly IDateTimeProvider _clock;
    private readonly IPasswordPolicyService _passwordPolicyService;
    private readonly IApplicationDbContext _dbContext;

    public LoginQueryHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IUserRefreshTokenRepository refreshTokenRepository,
        IUserLoginHistoryRepository loginHistoryRepository,
        IDateTimeProvider clock,
        IPasswordPolicyService passwordPolicyService,
        IApplicationDbContext dbContext)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _loginHistoryRepository = loginHistoryRepository;
        _clock = clock;
        _passwordPolicyService = passwordPolicyService;
        _dbContext = dbContext;
    }

    public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            throw new UnauthorizedException("Invalid credentials.");

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (!signInResult.Succeeded)
            throw new UnauthorizedException("Invalid credentials.");

        var roles = await _userManager.GetRolesAsync(user);

        var additionalClaims = new List<Claim>();
        var participantProfile = _dbContext.Participants.FirstOrDefault(p => p.ApplicationUserId == user.Id);
        if (participantProfile is not null)
            additionalClaims.Add(new Claim("participant_type", participantProfile.ParticipantType.ToString()));

        var tokenPair = _tokenService.GenerateTokenPair(user, roles, additionalClaims);

        var refreshToken = new UserRefreshToken(user.Id, tokenPair.RefreshTokenHash, tokenPair.RefreshTokenExpiresAt);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

        await _loginHistoryRepository.AddAsync(new UserLoginHistory
        {
            UserId = user.Id,
            OccurredAt = _clock.UtcNow,
            Success = true
        }, cancellationToken);
        await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

        var mustChangePassword = user.MustChangePasswordOnNextLogin || _passwordPolicyService.IsPasswordExpired(user, _clock.UtcNow, 90);

        return new LoginResponseDto(
            tokenPair.AccessToken,
            tokenPair.AccessTokenExpiresAt,
            tokenPair.RefreshToken,
            tokenPair.RefreshTokenExpiresAt,
            mustChangePassword);
    }
}
