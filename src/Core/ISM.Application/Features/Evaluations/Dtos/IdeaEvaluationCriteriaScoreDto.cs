namespace ISM.Application.Features.Evaluations.Dtos;

public record IdeaEvaluationCriteriaScoreDto(Guid CriteriaId, string Title, double Weight, int MinScore, int MaxScore);
