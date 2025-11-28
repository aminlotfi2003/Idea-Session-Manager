using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Event;

namespace ISM.WebApp.Services.ApiClients;

public class EventsApiClient : ApiClientBase, IEventsApiClient
{
    public EventsApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync("api/events", cancellationToken);
        return await ReadAsAsync<IEnumerable<EventDto>>(response) ?? Enumerable.Empty<EventDto>();
    }

    public async Task<EventDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"api/events/{id}", cancellationToken);
        return await ReadAsAsync<EventDto>(response);
    }

    public async Task CreateAsync(EventCreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync("api/events", request, cancellationToken);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(Guid id, EventUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PutAsJsonAsync($"api/events/{id}", request, cancellationToken);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync($"api/events/{id}", cancellationToken);
        await EnsureSuccess(response);
    }
}
