using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Evaluations;

public record SubmitIdeaEvaluationDto
{
    public Guid IdeaId { get; init; }
    public OverallDecision Decision { get; init; }
    public string? Comments { get; init; }
    public ICollection<SubmitEvaluationScoreDto> Scores { get; init; } = new List<SubmitEvaluationScoreDto>();
}
