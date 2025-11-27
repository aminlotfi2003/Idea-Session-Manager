using MediatR;

namespace ISM.Application.Features.Events.Commands.CloseIdeaSubmission;

public record CloseIdeaSubmissionCommand(Guid EventId) : IRequest;
