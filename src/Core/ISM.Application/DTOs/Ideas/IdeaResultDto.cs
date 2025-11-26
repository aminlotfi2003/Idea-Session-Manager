using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Ideas;

public record IdeaResultDto(Guid IdeaId, string IdeaCode, double? FinalScore, int? Rank, FinalDecision FinalDecision);
