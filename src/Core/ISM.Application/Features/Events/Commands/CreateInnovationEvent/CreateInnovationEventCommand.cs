using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Commands.CreateInnovationEvent;

public record CreateInnovationEventCommand(CreateInnovationEventDto Event) : IRequest<InnovationEventDetailDto>;
