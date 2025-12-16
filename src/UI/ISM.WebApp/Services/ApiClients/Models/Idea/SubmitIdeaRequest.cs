namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class SubmitIdeaRequest
{
    public Guid EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string ProposedImplementation { get; set; } = string.Empty;
    public string ValueProposition { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    public string ParticipantEmail { get; set; } = string.Empty;
    public string ParticipantDepartment { get; set; } = string.Empty;
}
