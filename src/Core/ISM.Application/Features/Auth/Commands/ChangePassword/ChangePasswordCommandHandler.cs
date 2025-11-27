using ISM.Application.Common.Abstractions.Services;
using ISM.Application.Features.Auth.Dtos;
using ISM.Domain.Identity;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ISM.Application.Features.Auth.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordPolicyService _passwordPolicyService;
    private readonly IDateTimeProvider _clock;

    public ChangePasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IPasswordPolicyService passwordPolicyService,
        IDateTimeProvider clock)
    {
        _userManager = userManager;
        _passwordPolicyService = passwordPolicyService;
        _clock = clock;
    }

    public async Task<ChangePasswordResultDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            throw new NotFoundException("User not found.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!passwordValid)
            throw new UnauthorizedException("Invalid password.");

        await _passwordPolicyService.EnsurePasswordCompliesAsync(user, request.NewPassword, cancellationToken);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        user.PasswordLastChangedAt = _clock.UtcNow;
        user.MustChangePasswordOnNextLogin = false;
        await _userManager.UpdateAsync(user);

        if (user.PasswordHash is not null)
            await _passwordPolicyService.RecordPasswordChangeAsync(user, user.PasswordHash, cancellationToken);

        return new ChangePasswordResultDto(true);
    }
}
