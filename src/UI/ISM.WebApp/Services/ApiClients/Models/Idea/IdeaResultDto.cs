using ISM.WebApp.Services.ApiClients.Models.Common;

namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class IdeaResultDto
{
    public Guid IdeaId { get; set; }
    public string IdeaCode { get; set; } = string.Empty;
    public double? FinalScore { get; set; }
    public int? Rank { get; set; }
    public FinalDecision FinalDecision { get; set; }
}
