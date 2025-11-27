using ISM.Domain.Enums;

namespace ISM.Application.Features.Events.Dtos;

public record EventSummaryDto
{
    public Guid EventId { get; init; }
    public string Title { get; init; } = string.Empty;
    public EventStatus Status { get; init; }
    public int IdeaCount { get; init; }
    public int JudgeCount { get; init; }
}
