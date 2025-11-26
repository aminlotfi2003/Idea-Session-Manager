using FluentValidation;

namespace ISM.Application.DTOs.Ideas;

public record IdeaAssignmentDto(Guid IdeaId, Guid JudgeId);

public class IdeaAssignmentDtoValidator : AbstractValidator<IdeaAssignmentDto>
{
    public IdeaAssignmentDtoValidator()
    {
        RuleFor(x => x.IdeaId).NotEmpty();
        RuleFor(x => x.JudgeId).NotEmpty();
    }
}
