using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeaDetails;

public record GetMyIdeaDetailsQuery(Guid IdeaId, Guid CurrentUserId) : IRequest<IdeaDetailDto>;
