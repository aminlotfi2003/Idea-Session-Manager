using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventPublicDetails;

public record GetEventPublicDetailsQuery(Guid EventId) : IRequest<InnovationEventDetailDto>;
