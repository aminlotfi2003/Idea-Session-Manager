using FluentValidation;
using ISM.Application.Features.Ideas.Dtos;

namespace ISM.Application.Features.Ideas.Commands.AssignIdeasToJudges;

public class AssignIdeasToJudgesCommandValidator : AbstractValidator<AssignIdeasToJudgesCommand>
{
    public AssignIdeasToJudgesCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.Assignments).NotEmpty();
        RuleForEach(x => x.Assignments).SetValidator(new IdeaAssignmentDtoValidator());
    }
}
