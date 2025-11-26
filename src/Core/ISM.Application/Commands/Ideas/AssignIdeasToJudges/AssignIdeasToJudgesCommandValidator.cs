using FluentValidation;
using ISM.Application.DTOs.Ideas;

namespace ISM.Application.Commands.Ideas.AssignIdeasToJudges;

public class AssignIdeasToJudgesCommandValidator : AbstractValidator<AssignIdeasToJudgesCommand>
{
    public AssignIdeasToJudgesCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.Assignments).NotEmpty();
        RuleForEach(x => x.Assignments).SetValidator(new IdeaAssignmentDtoValidator());
    }
}
