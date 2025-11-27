using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventDetails;

public record GetEventDetailsQuery(Guid EventId) : IRequest<InnovationEventDetailDto>;
