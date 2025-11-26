using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetMyIdeaDetails;

public record GetMyIdeaDetailsQuery(Guid IdeaId, Guid CurrentUserId) : IRequest<IdeaDetailDto>;
