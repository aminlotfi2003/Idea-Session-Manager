using ISM.Application.DTOs.Ideas;
using MediatR;

namespace ISM.Application.Commands.Ideas.SubmitIdea;

public record SubmitIdeaCommand(SubmitIdeaDto Idea, Guid CurrentUserId) : IRequest<IdeaDetailDto>;
