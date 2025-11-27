using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Commands.PublishEvent;

public record PublishEventCommand(Guid EventId) : IRequest<InnovationEventDetailDto>;
