namespace ISM.Application.DTOs.Events;

public record DefineEvaluationCriteriaDto(Guid EventId, IEnumerable<EvaluationCriteriaDto> Criteria);
