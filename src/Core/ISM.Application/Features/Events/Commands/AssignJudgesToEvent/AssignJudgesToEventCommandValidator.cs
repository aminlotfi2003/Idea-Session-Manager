using FluentValidation;

namespace ISM.Application.Features.Events.Commands.AssignJudgesToEvent;

public class AssignJudgesToEventCommandValidator : AbstractValidator<AssignJudgesToEventCommand>
{
    public AssignJudgesToEventCommandValidator()
    {
        RuleFor(x => x.Payload.EventId).NotEmpty();
        RuleFor(x => x.Payload.JudgeIds).NotEmpty();
    }
}
