using ISM.Application.Features.Evaluations.Dtos;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;

public record GetAssignedIdeasForJudgeQuery(Guid JudgeId, Guid EventId) : IRequest<IReadOnlyCollection<JudgeAssignedIdeaDto>>;
