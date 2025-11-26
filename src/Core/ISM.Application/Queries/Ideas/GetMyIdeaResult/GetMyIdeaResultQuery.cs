using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetMyIdeaResult;

public record GetMyIdeaResultQuery(Guid IdeaId, Guid CurrentUserId) : IRequest<IdeaResultDto>;
