using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeaResult;

public record GetMyIdeaResultQuery(Guid IdeaId, Guid CurrentUserId) : IRequest<IdeaResultDto>;
