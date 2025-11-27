namespace ISM.Application.Features.Events.Dtos;

public record AssignJudgesDto(Guid EventId, IEnumerable<Guid> JudgeIds);
