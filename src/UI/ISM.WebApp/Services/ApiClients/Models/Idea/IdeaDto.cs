namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class IdeaDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
