using ISM.Application.Abstractions.Persistence;
using ISM.Application.Abstractions.Services;
using ISM.Application.DTOs.Auth;
using ISM.Domain.Entities;
using ISM.Domain.Identity;
using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ISM.Application.Commands.Auth.RegisterParticipant;

public sealed class RegisterParticipantCommandHandler : IRequestHandler<RegisterParticipantCommand, ParticipantRegistrationResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _clock;
    private readonly IPasswordPolicyService _passwordPolicyService;

    public RegisterParticipantCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IApplicationDbContext dbContext,
        IDateTimeProvider clock,
        IPasswordPolicyService passwordPolicyService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _clock = clock;
        _passwordPolicyService = passwordPolicyService;
    }

    public async Task<ParticipantRegistrationResultDto> Handle(RegisterParticipantCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new ConflictException("Email already registered.");

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            PasswordLastChangedAt = _clock.UtcNow,
            MustChangePasswordOnNextLogin = false,
            CreatedAt = _clock.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(",", createResult.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        if (!await _roleManager.RoleExistsAsync(ApplicationRoles.Participant))
            await _roleManager.CreateAsync(new ApplicationRole { Name = ApplicationRoles.Participant });

        await _userManager.AddToRoleAsync(user, ApplicationRoles.Participant);

        var profile = ParticipantProfile.Create(
            request.FullName,
            request.ParticipantType.ToString(),
            ParticipantContactInfo.Create(request.Email, string.Empty),
            request.ParticipantType,
            _clock.UtcNow,
            user.Id
        );

        await _dbContext.Participants.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (user.PasswordHash is not null)
            await _passwordPolicyService.RecordPasswordChangeAsync(user, user.PasswordHash, cancellationToken);

        return new ParticipantRegistrationResultDto(user.Id, profile.Id, user.Email!, request.FullName, request.ParticipantType.ToString());
    }
}
