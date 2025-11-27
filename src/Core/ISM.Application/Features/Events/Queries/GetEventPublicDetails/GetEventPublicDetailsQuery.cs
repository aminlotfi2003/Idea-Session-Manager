using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventPublicDetails;

public record GetEventPublicDetailsQuery(Guid EventId) : IRequest<InnovationEventDetailDto>;
