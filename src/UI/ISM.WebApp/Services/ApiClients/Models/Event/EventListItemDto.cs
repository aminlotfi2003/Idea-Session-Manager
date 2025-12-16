using ISM.WebApp.Services.ApiClients.Models.Common;

namespace ISM.WebApp.Services.ApiClients.Models.Event;

public class EventListItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public EventStatus Status { get; set; }
    public DateTimeOffset IdeaSubmissionStart { get; set; }
    public DateTimeOffset IdeaSubmissionEnd { get; set; }
    public AllowedParticipantGroup AllowedParticipantGroup { get; set; }
}
