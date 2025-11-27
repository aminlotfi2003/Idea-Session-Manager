using MediatR;

namespace ISM.Application.Features.Events.Commands.FinalizeEventEvaluation;

public record FinalizeEventEvaluationCommand(Guid EventId) : IRequest;
