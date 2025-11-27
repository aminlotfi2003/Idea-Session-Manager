using FluentValidation;

namespace ISM.Application.Features.Ideas.Dtos;

public record IdeaAssignmentDto(Guid IdeaId, Guid JudgeId);

public class IdeaAssignmentDtoValidator : AbstractValidator<IdeaAssignmentDto>
{
    public IdeaAssignmentDtoValidator()
    {
        RuleFor(x => x.IdeaId).NotEmpty();
        RuleFor(x => x.JudgeId).NotEmpty();
    }
}
