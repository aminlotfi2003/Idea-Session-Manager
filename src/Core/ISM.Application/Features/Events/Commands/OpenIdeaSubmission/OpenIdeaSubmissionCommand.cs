using MediatR;

namespace ISM.Application.Features.Events.Commands.OpenIdeaSubmission;

public record OpenIdeaSubmissionCommand(Guid EventId) : IRequest;
