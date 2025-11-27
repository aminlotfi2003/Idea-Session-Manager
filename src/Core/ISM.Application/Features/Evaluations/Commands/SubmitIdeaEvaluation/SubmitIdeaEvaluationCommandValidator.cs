using FluentValidation;
using ISM.Application.Features.Evaluations.Dtos;

namespace ISM.Application.Features.Evaluations.Commands.SubmitIdeaEvaluation;

public class SubmitIdeaEvaluationCommandValidator : AbstractValidator<SubmitIdeaEvaluationCommand>
{
    public SubmitIdeaEvaluationCommandValidator()
    {
        RuleFor(x => x.JudgeId).NotEmpty();
        RuleFor(x => x.Evaluation.IdeaId).NotEmpty();
        RuleForEach(x => x.Evaluation.Scores).SetValidator(new SubmitEvaluationScoreDtoValidator());
    }
}
