using FluentValidation;
using ISM.Application.Features.Events.Dtos;

namespace ISM.Application.Features.Events.Commands.DefineOrUpdateEvaluationCriteria;

public class DefineOrUpdateEvaluationCriteriaCommandValidator : AbstractValidator<DefineOrUpdateEvaluationCriteriaCommand>
{
    public DefineOrUpdateEvaluationCriteriaCommandValidator()
    {
        RuleFor(x => x.Payload.EventId).NotEmpty();
        RuleForEach(x => x.Payload.Criteria).SetValidator(new EvaluationCriteriaDtoValidator());
    }
}
