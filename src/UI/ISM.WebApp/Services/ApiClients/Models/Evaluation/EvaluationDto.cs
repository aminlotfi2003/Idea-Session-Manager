namespace ISM.WebApp.Services.ApiClients.Models.Evaluation;

public class EvaluationDto
{
    public Guid Id { get; set; }
    public Guid IdeaId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
