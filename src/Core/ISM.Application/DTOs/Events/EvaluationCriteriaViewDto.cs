namespace ISM.Application.DTOs.Events;

public record EvaluationCriteriaViewDto(Guid Id, string Name, string Description, double Weight, double MinScore, double MaxScore);
