using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Ideas;

public record IdeaDetailDto
{
    public Guid Id { get; init; }
    public string IdeaCode { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Requirements { get; init; } = string.Empty;
    public string ProposedImplementation { get; init; } = string.Empty;
    public string ValueProposition { get; init; } = string.Empty;
    public IdeaStatus Status { get; init; }
    public double? FinalScore { get; init; }
    public FinalDecision FinalDecision { get; init; }
}
