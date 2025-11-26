using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetEventResultsForAdmin;

public record GetEventResultsForAdminQuery(Guid EventId) : IRequest<IReadOnlyCollection<IdeaResultDto>>;
