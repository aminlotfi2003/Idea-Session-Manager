using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Idea;

namespace ISM.WebApp.Services.ApiClients;

public class IdeasApiClient : ApiClientBase, IIdeasApiClient
{
    public IdeasApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<IdeaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync("api/ideas", cancellationToken);
        return await ReadAsAsync<IEnumerable<IdeaDto>>(response) ?? Enumerable.Empty<IdeaDto>();
    }

    public async Task<IdeaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"api/ideas/{id}", cancellationToken);
        return await ReadAsAsync<IdeaDto>(response);
    }

    public async Task CreateAsync(IdeaCreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync("api/ideas", request, cancellationToken);
        await EnsureSuccess(response);
    }

    public async Task UpdateAsync(Guid id, IdeaUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PutAsJsonAsync($"api/ideas/{id}", request, cancellationToken);
        await EnsureSuccess(response);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync($"api/ideas/{id}", cancellationToken);
        await EnsureSuccess(response);
    }
}
