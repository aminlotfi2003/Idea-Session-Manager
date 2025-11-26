using FluentValidation;

namespace ISM.Application.Commands.Auth.RegisterParticipant;

public class RegisterParticipantCommandValidator : AbstractValidator<RegisterParticipantCommand>
{
    public RegisterParticipantCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(c => c.FullName).NotEmpty();
        RuleFor(c => c.ParticipantType).IsInEnum();
    }
}
