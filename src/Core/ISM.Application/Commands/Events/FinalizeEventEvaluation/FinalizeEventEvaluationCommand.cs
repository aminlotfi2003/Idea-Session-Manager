using MediatR;

namespace ISM.Application.Commands.Events.FinalizeEventEvaluation;

public record FinalizeEventEvaluationCommand(Guid EventId) : IRequest;
