using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventList;

public record GetEventListQuery(EventStatus? Status) : IRequest<IReadOnlyCollection<InnovationEventListItemDto>>;
