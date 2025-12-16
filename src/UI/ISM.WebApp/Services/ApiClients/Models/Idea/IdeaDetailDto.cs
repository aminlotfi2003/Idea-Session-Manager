using ISM.WebApp.Services.ApiClients.Models.Common;

namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class IdeaDetailDto
{
    public Guid Id { get; set; }
    public string IdeaCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string ProposedImplementation { get; set; } = string.Empty;
    public string ValueProposition { get; set; } = string.Empty;
    public IdeaStatus Status { get; set; }
    public double? FinalScore { get; set; }
    public FinalDecision FinalDecision { get; set; }
}
