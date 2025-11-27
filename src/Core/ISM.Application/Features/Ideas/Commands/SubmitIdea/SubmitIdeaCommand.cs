using ISM.Application.Features.Ideas.Dtos;
using MediatR;

namespace ISM.Application.Features.Ideas.Commands.SubmitIdea;

public record SubmitIdeaCommand(SubmitIdeaDto Idea, Guid CurrentUserId) : IRequest<IdeaDetailDto>;
