using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Commands.AssignIdeasToJudges;

public record AssignIdeasToJudgesCommand(Guid EventId, IEnumerable<IdeaAssignmentDto> Assignments) : IRequest;
