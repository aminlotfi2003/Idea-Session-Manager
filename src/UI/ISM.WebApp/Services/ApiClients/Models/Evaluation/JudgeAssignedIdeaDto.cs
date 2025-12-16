namespace ISM.WebApp.Services.ApiClients.Models.Evaluation;

public class JudgeAssignedIdeaDto
{
    public Guid IdeaId { get; set; }
    public string IdeaCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
