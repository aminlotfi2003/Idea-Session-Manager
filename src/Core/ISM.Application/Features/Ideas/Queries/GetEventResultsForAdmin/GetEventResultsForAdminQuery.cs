using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetEventResultsForAdmin;

public record GetEventResultsForAdminQuery(Guid EventId, PaginationParams Pagination) : IRequest<PaginatedResult<IdeaResultDto>>;
