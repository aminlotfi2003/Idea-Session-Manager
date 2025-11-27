using ISM.Domain.Enums;

namespace ISM.Application.Features.Ideas.Dtos;

public record IdeaListItemDto
{
    public Guid Id { get; init; }
    public string IdeaCode { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public IdeaStatus Status { get; init; }
    public double? FinalScore { get; init; }
    public FinalDecision FinalDecision { get; init; }
}
