using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Commands.Events.UpdateInnovationEvent;

public record UpdateInnovationEventCommand(UpdateInnovationEventDto Event) : IRequest<InnovationEventDetailDto>;
