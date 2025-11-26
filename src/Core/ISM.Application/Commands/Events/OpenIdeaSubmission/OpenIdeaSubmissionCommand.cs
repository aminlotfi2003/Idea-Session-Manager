using MediatR;

namespace ISM.Application.Commands.Events.OpenIdeaSubmission;

public record OpenIdeaSubmissionCommand(Guid EventId) : IRequest;
