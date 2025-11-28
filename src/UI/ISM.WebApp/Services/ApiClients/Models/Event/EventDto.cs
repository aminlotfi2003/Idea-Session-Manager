namespace ISM.WebApp.Services.ApiClients.Models.Event;

public class EventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset? StartDate { get; set; }
}
