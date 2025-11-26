using MediatR;

namespace ISM.Application.Commands.Events.CloseIdeaSubmission;

public record CloseIdeaSubmissionCommand(Guid EventId) : IRequest;
