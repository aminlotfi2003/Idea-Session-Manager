namespace ISM.Application.Features.Evaluations.Dtos;

public record JudgeAssignedIdeaDto
{
    public Guid IdeaId { get; init; }
    public string IdeaCode { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
