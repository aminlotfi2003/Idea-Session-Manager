namespace ISM.Application.DTOs.Evaluations;

public record IdeaEvaluationCriteriaScoreDto(Guid CriteriaId, string Title, double Weight, int MinScore, int MaxScore);
