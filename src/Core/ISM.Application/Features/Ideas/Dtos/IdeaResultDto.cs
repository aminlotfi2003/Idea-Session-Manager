using ISM.Domain.Enums;

namespace ISM.Application.Features.Ideas.Dtos;

public record IdeaResultDto(Guid IdeaId, string IdeaCode, double? FinalScore, int? Rank, FinalDecision FinalDecision);
