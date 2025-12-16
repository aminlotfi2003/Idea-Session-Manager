using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Event;
using Microsoft.AspNetCore.WebUtilities;

namespace ISM.WebApp.Services.ApiClients;

public class EventsApiClient : ApiClientBase, IEventsApiClient
{
    public EventsApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<PaginatedResult<EventListItemDto>?> GetAdminEventsAsync(EventStatus? status, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["pageNumber"] = pageNumber.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["status"] = status.HasValue ? ((int)status.Value).ToString() : null
        };

        var url = QueryHelpers.AddQueryString("events", queryParams!);
        var response = await HttpClient.GetAsync(url, cancellationToken);
        return await ReadAsAsync<PaginatedResult<EventListItemDto>>(response);
    }

    public async Task<PaginatedResult<EventListItemDto>?> GetOpenEventsAsync(AllowedParticipantGroup group, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["group"] = ((int)group).ToString(),
            ["pageNumber"] = pageNumber.ToString(),
            ["pageSize"] = pageSize.ToString()
        };

        var url = QueryHelpers.AddQueryString("events/open", queryParams!);
        var response = await HttpClient.GetAsync(url, cancellationToken);
        return await ReadAsAsync<PaginatedResult<EventListItemDto>>(response);
    }

    public async Task<EventDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"events/{id}", cancellationToken);
        return await ReadAsAsync<EventDetailDto>(response);
    }
}
