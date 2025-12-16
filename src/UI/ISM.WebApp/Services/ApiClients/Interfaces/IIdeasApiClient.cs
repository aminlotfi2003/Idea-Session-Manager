using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Idea;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IIdeasApiClient
{
    Task<PaginatedResult<IdeaListItemDto>?> GetMyIdeasForEventAsync(Guid eventId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<IdeaDetailDto?> GetMyIdeaAsync(Guid ideaId, CancellationToken cancellationToken = default);
    Task<IdeaResultDto?> GetMyResultAsync(Guid ideaId, CancellationToken cancellationToken = default);
    Task<IdeaDetailDto?> SubmitAsync(SubmitIdeaRequest request, CancellationToken cancellationToken = default);
}
