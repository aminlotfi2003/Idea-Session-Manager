using ISM.WebApp.Services.ApiClients.Models.Event;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IEventsApiClient
{
    Task<IEnumerable<EventDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EventDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(EventCreateRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, EventUpdateRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
