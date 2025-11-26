using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventDetails;

public record GetEventDetailsQuery(Guid EventId) : IRequest<InnovationEventDetailDto>;
