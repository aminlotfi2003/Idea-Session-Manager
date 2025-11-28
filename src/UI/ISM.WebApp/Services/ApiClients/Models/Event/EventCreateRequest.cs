namespace ISM.WebApp.Services.ApiClients.Models.Event;

public class EventCreateRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset? StartDate { get; set; }
}
