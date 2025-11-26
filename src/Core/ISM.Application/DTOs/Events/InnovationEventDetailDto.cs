using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Events;

public record InnovationEventDetailDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Goals { get; init; } = string.Empty;
    public AllowedParticipantGroup AllowedParticipantGroup { get; init; }
    public DateTimeOffset IdeaSubmissionStart { get; init; }
    public DateTimeOffset IdeaSubmissionEnd { get; init; }
    public EventStatus Status { get; init; }
    public string? RulesDocumentPath { get; init; }
    public IReadOnlyCollection<EventJudgeDto> Judges { get; init; } = Array.Empty<EventJudgeDto>();
    public IReadOnlyCollection<EvaluationCriteriaViewDto> Criteria { get; init; } = Array.Empty<EvaluationCriteriaViewDto>();
}
