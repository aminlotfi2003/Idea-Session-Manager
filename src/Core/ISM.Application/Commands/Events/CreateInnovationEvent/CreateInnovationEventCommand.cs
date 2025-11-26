using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Commands.Events.CreateInnovationEvent;

public record CreateInnovationEventCommand(CreateInnovationEventDto Event) : IRequest<InnovationEventDetailDto>;
