using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Commands.Events.AssignJudgesToEvent;

public record AssignJudgesToEventCommand(AssignJudgesDto Payload) : IRequest;
