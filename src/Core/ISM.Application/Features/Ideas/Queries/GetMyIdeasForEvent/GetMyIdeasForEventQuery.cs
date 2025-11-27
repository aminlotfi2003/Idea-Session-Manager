using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;

public record GetMyIdeasForEventQuery(Guid EventId, Guid CurrentUserId) : IRequest<IReadOnlyCollection<IdeaListItemDto>>;
