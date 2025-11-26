using FluentValidation;

namespace ISM.Application.Commands.Auth.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(c => c.RefreshToken).NotEmpty();
    }
}
