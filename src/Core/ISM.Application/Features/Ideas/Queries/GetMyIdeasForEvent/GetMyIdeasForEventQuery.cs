using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;

public record GetMyIdeasForEventQuery(Guid EventId, Guid CurrentUserId, PaginationParams Pagination) : IRequest<PaginatedResult<IdeaListItemDto>>;
