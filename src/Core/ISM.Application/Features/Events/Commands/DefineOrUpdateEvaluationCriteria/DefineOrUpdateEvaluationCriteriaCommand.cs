using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Commands.DefineOrUpdateEvaluationCriteria;

public record DefineOrUpdateEvaluationCriteriaCommand(DefineEvaluationCriteriaDto Payload) : IRequest;
