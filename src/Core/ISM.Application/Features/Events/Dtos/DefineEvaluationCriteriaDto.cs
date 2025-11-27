namespace ISM.Application.Features.Events.Dtos;

public record DefineEvaluationCriteriaDto(Guid EventId, IEnumerable<EvaluationCriteriaDto> Criteria);
