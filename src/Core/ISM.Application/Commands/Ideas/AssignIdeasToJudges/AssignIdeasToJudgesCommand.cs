using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Commands.Ideas.AssignIdeasToJudges;

public record AssignIdeasToJudgesCommand(Guid EventId, IEnumerable<IdeaAssignmentDto> Assignments) : IRequest;
