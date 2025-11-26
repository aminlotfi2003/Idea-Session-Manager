using ISM.Application.DTOs.Events;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Queries.Events.GetOpenEventsForParticipant;

public record GetOpenEventsForParticipantQuery(AllowedParticipantGroup AllowedGroup) : IRequest<IReadOnlyCollection<InnovationEventListItemDto>>;
