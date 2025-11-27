using FluentValidation;

namespace ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;

public class GetAssignedIdeasForJudgeQueryValidator : AbstractValidator<GetAssignedIdeasForJudgeQuery>
{
    public GetAssignedIdeasForJudgeQueryValidator()
    {
        RuleFor(x => x.JudgeId).NotEmpty();
        RuleFor(x => x.EventId).NotEmpty();
    }
}
