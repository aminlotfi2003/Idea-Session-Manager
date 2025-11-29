using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetOpenEventsForParticipant;

public record GetOpenEventsForParticipantQuery(AllowedParticipantGroup AllowedGroup, PaginationParams Pagination) : IRequest<PaginatedResult<InnovationEventListItemDto>>;
