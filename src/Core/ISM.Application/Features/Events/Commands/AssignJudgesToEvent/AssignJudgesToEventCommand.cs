using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Commands.AssignJudgesToEvent;

public record AssignJudgesToEventCommand(AssignJudgesDto Payload) : IRequest;
