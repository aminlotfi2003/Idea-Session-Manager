using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetOpenEventsForParticipant;

public record GetOpenEventsForParticipantQuery(AllowedParticipantGroup AllowedGroup) : IRequest<IReadOnlyCollection<InnovationEventListItemDto>>;
