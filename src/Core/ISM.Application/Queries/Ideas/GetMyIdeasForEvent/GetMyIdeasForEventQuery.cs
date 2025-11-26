using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetMyIdeasForEvent;

public record GetMyIdeasForEventQuery(Guid EventId, Guid CurrentUserId) : IRequest<IReadOnlyCollection<IdeaListItemDto>>;
