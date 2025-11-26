using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Commands.Events.DefineOrUpdateEvaluationCriteria;

public record DefineOrUpdateEvaluationCriteriaCommand(DefineEvaluationCriteriaDto Payload) : IRequest;
