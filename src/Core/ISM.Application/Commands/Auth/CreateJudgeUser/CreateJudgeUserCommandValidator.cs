using FluentValidation;

namespace ISM.Application.Commands.Auth.CreateJudgeUser;

public class CreateJudgeUserCommandValidator : AbstractValidator<CreateJudgeUserCommand>
{
    public CreateJudgeUserCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.TemporaryPassword).NotEmpty().MinimumLength(8);
    }
}
