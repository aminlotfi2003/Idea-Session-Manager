using FluentValidation;
using ISM.Application.DTOs.Events;

namespace ISM.Application.Commands.Events.DefineOrUpdateEvaluationCriteria;

public class DefineOrUpdateEvaluationCriteriaCommandValidator : AbstractValidator<DefineOrUpdateEvaluationCriteriaCommand>
{
    public DefineOrUpdateEvaluationCriteriaCommandValidator()
    {
        RuleFor(x => x.Payload.EventId).NotEmpty();
        RuleForEach(x => x.Payload.Criteria).SetValidator(new EvaluationCriteriaDtoValidator());
    }
}
