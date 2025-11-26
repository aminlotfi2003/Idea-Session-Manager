namespace ISM.Application.DTOs.Events;

public record AssignJudgesDto(Guid EventId, IEnumerable<Guid> JudgeIds);
