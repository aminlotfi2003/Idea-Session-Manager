using ISM.WebApp.Services.ApiClients.Models.Common;

namespace ISM.WebApp.Services.ApiClients.Models.Idea;

public class IdeaListItemDto
{
    public Guid Id { get; set; }
    public string IdeaCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public IdeaStatus Status { get; set; }
}
