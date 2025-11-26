using FluentValidation;
using ISM.Application.DTOs.Evaluations;

namespace ISM.Application.Commands.Evaluations.SubmitIdeaEvaluation;

public class SubmitIdeaEvaluationCommandValidator : AbstractValidator<SubmitIdeaEvaluationCommand>
{
    public SubmitIdeaEvaluationCommandValidator()
    {
        RuleFor(x => x.JudgeId).NotEmpty();
        RuleFor(x => x.Evaluation.IdeaId).NotEmpty();
        RuleForEach(x => x.Evaluation.Scores).SetValidator(new SubmitEvaluationScoreDtoValidator());
    }
}
