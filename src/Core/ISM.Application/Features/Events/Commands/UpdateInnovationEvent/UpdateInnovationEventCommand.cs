using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Commands.UpdateInnovationEvent;

public record UpdateInnovationEventCommand(UpdateInnovationEventDto Event) : IRequest<InnovationEventDetailDto>;
