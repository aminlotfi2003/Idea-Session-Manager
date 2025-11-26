using ISM.Application.Abstractions.Persistence;
using ISM.Application.DTOs.Auth;
using ISM.Domain.Entities;
using ISM.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ISM.Application.Commands.Auth.CreateJudgeUser;

public sealed class CreateJudgeUserCommandHandler : IRequestHandler<CreateJudgeUserCommand, JudgeCreatedDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IApplicationDbContext _dbContext;

    public CreateJudgeUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<JudgeCreatedDto> Handle(CreateJudgeUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            MustChangePasswordOnNextLogin = true,
            CreatedAt = DateTimeOffset.UtcNow,
            PasswordLastChangedAt = DateTimeOffset.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, request.TemporaryPassword);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(",", createResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }

        if (!await _roleManager.RoleExistsAsync(ApplicationRoles.Judge))
        {
            await _roleManager.CreateAsync(new ApplicationRole { Name = ApplicationRoles.Judge });
        }

        await _userManager.AddToRoleAsync(user, ApplicationRoles.Judge);

        var judge = Judge.Create(user.Id, request.FullName, string.Empty);
        await _dbContext.Judges.AddAsync(judge, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new JudgeCreatedDto(user.Id, user.Email!, request.TemporaryPassword);
    }
}
