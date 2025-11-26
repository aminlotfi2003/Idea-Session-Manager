using ISM.Application.DTOs.Events;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventList;

public record GetEventListQuery(EventStatus? Status) : IRequest<IReadOnlyCollection<InnovationEventListItemDto>>;
