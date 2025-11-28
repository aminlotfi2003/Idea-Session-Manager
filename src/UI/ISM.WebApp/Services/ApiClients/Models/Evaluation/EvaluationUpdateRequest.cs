namespace ISM.WebApp.Services.ApiClients.Models.Evaluation;

public class EvaluationUpdateRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
