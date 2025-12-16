using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Idea;
using Microsoft.AspNetCore.WebUtilities;

namespace ISM.WebApp.Services.ApiClients;

public class IdeasApiClient : ApiClientBase, IIdeasApiClient
{
    public IdeasApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IdeaDetailDto?> SubmitAsync(SubmitIdeaRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync("ideas", request, cancellationToken);
        return await ReadAsAsync<IdeaDetailDto>(response);
    }

    public async Task<PaginatedResult<IdeaListItemDto>?> GetMyIdeasForEventAsync(Guid eventId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["pageNumber"] = pageNumber.ToString(),
            ["pageSize"] = pageSize.ToString()
        };

        var url = QueryHelpers.AddQueryString($"ideas/{eventId}/mine", queryParams!);
        var response = await HttpClient.GetAsync(url, cancellationToken);
        return await ReadAsAsync<PaginatedResult<IdeaListItemDto>>(response);
    }

    public async Task<IdeaDetailDto?> GetMyIdeaAsync(Guid ideaId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"ideas/{ideaId}", cancellationToken);
        return await ReadAsAsync<IdeaDetailDto>(response);
    }

    public async Task<IdeaResultDto?> GetMyResultAsync(Guid ideaId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync($"ideas/{ideaId}/my-result", cancellationToken);
        return await ReadAsAsync<IdeaResultDto>(response);
    }
}
