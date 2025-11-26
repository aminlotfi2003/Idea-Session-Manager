using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Commands.Events.PublishEvent;

public record PublishEventCommand(Guid EventId) : IRequest<InnovationEventDetailDto>;
