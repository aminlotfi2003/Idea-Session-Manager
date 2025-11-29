using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventList;

public record GetEventListQuery(EventStatus? Status, PaginationParams Pagination) : IRequest<PaginatedResult<InnovationEventListItemDto>>;
