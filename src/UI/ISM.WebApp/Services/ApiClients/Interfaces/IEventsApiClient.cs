using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Event;

namespace ISM.WebApp.Services.ApiClients.Interfaces;

public interface IEventsApiClient
{
    Task<PaginatedResult<EventListItemDto>?> GetAdminEventsAsync(EventStatus? status, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<EventListItemDto>?> GetOpenEventsAsync(AllowedParticipantGroup group, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<EventDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
