using FluentValidation;

namespace ISM.Application.DTOs.Evaluations;

public record SubmitEvaluationScoreDto(Guid CriteriaId, int Score, string? Comment);

public class SubmitEvaluationScoreDtoValidator : AbstractValidator<SubmitEvaluationScoreDto>
{
    public SubmitEvaluationScoreDtoValidator()
    {
        RuleFor(x => x.CriteriaId).NotEmpty();
    }
}
