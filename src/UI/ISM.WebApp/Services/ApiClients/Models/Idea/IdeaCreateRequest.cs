namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class IdeaCreateRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
