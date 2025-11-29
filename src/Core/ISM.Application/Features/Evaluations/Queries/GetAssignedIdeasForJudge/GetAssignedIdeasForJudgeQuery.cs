using ISM.Application.Features.Evaluations.Dtos;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;

public record GetAssignedIdeasForJudgeQuery(Guid JudgeId, Guid EventId, PaginationParams Pagination) : IRequest<PaginatedResult<JudgeAssignedIdeaDto>>;
