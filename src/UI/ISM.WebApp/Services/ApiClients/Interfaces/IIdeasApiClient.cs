using ISM.WebApp.Services.ApiClients.Models.Idea;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IIdeasApiClient
{
    Task<IEnumerable<IdeaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IdeaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(IdeaCreateRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, IdeaUpdateRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
