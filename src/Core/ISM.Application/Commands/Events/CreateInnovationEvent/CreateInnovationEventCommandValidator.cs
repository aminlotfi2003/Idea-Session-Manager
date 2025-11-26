using FluentValidation;

namespace ISM.Application.Commands.Events.CreateInnovationEvent;

public class CreateInnovationEventCommandValidator : AbstractValidator<CreateInnovationEventCommand>
{
    public CreateInnovationEventCommandValidator()
    {
        RuleFor(x => x.Event.Title).NotEmpty();
        RuleFor(x => x.Event.IdeaSubmissionEnd).GreaterThan(x => x.Event.IdeaSubmissionStart);
        RuleFor(x => x.Event.AllowedParticipantGroup).IsInEnum();
    }
}
