using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetEventResultsForAdmin;

public record GetEventResultsForAdminQuery(Guid EventId) : IRequest<IReadOnlyCollection<IdeaResultDto>>;
