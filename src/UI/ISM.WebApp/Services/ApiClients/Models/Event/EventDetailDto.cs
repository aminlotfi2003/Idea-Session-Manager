using ISM.WebApp.Services.ApiClients.Models.Common;

namespace ISM.WebApp.Services.ApiClients.Models.Event;

public class EventDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Goals { get; set; } = string.Empty;
    public AllowedParticipantGroup AllowedParticipantGroup { get; set; }
    public DateTimeOffset IdeaSubmissionStart { get; set; }
    public DateTimeOffset IdeaSubmissionEnd { get; set; }
    public EventStatus Status { get; set; }
    public string? RulesDocumentPath { get; set; }
    public IReadOnlyCollection<EventJudgeDto> Judges { get; set; } = Array.Empty<EventJudgeDto>();
    public IReadOnlyCollection<EvaluationCriteriaViewDto> Criteria { get; set; } = Array.Empty<EvaluationCriteriaViewDto>();
}
