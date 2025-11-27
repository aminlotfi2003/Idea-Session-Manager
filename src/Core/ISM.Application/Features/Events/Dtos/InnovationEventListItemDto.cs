using ISM.Domain.Enums;

namespace ISM.Application.Features.Events.Dtos;

public record InnovationEventListItemDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public EventStatus Status { get; init; }
    public DateTimeOffset IdeaSubmissionStart { get; init; }
    public DateTimeOffset IdeaSubmissionEnd { get; init; }
    public AllowedParticipantGroup AllowedParticipantGroup { get; init; }
}
