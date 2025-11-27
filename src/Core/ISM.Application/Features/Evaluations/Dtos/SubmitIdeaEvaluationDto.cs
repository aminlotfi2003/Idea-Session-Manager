using ISM.Domain.Enums;

namespace ISM.Application.Features.Evaluations.Dtos;

public record SubmitIdeaEvaluationDto
{
    public Guid IdeaId { get; init; }
    public OverallDecision Decision { get; init; }
    public string? Comments { get; init; }
    public ICollection<SubmitEvaluationScoreDto> Scores { get; init; } = new List<SubmitEvaluationScoreDto>();
}
