namespace ISM.Application.Features.Events.Dtos;

public record EvaluationCriteriaViewDto(Guid Id, string Name, string Description, double Weight, double MinScore, double MaxScore);
