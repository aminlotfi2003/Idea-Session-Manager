namespace ISM.WebApp.Services.ApiClients.Models.Event;

public class EvaluationCriteriaViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Weight { get; set; }
}
