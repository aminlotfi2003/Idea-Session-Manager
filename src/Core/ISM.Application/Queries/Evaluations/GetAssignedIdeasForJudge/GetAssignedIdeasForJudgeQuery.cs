using ISM.Application.DTOs.Evaluations;
using MediatR;

namespace ISM.Application.Queries.Evaluations.GetAssignedIdeasForJudge;

public record GetAssignedIdeasForJudgeQuery(Guid JudgeId, Guid EventId) : IRequest<IReadOnlyCollection<JudgeAssignedIdeaDto>>;
