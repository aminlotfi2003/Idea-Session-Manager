using FluentValidation;

namespace ISM.Application.Features.Events.Dtos;

public record EvaluationCriteriaDto(Guid? Id, string Title, string Description, int MinScore, int MaxScore, double Weight, int Order);

public class EvaluationCriteriaDtoValidator : AbstractValidator<EvaluationCriteriaDto>
{
    public EvaluationCriteriaDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.MaxScore).GreaterThan(x => x.MinScore);
    }
}
