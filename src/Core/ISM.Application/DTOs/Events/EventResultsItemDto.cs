using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Events;

public record EventResultsItemDto
{
    public Guid IdeaId { get; init; }
    public string IdeaCode { get; init; } = string.Empty;
    public double? FinalScore { get; init; }
    public int? Rank { get; init; }
    public FinalDecision FinalDecision { get; init; }
    public Guid? ParticipantProfileId { get; init; }
}
