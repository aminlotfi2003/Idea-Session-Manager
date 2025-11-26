using FluentValidation;

namespace ISM.Application.Queries.Evaluations.GetIdeaForEvaluation;

public class GetIdeaForEvaluationQueryValidator : AbstractValidator<GetIdeaForEvaluationQuery>
{
    public GetIdeaForEvaluationQueryValidator()
    {
        RuleFor(x => x.IdeaId).NotEmpty();
        RuleFor(x => x.JudgeId).NotEmpty();
    }
}
