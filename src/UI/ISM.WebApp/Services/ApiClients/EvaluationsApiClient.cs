using ISM.WebApp.Services.ApiClients.Interfaces;
using ISM.WebApp.Services.ApiClients.Models.Common;
using ISM.WebApp.Services.ApiClients.Models.Evaluation;
using Microsoft.AspNetCore.WebUtilities;

namespace ISM.WebApp.Services.ApiClients;

public class EvaluationsApiClient : ApiClientBase, IEvaluationsApiClient
{
    public EvaluationsApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<PaginatedResult<JudgeAssignedIdeaDto>?> GetAssignedIdeasAsync(Guid eventId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["pageNumber"] = pageNumber.ToString(),
            ["pageSize"] = pageSize.ToString()
        };

        var url = QueryHelpers.AddQueryString($"evaluations/events/{eventId}/assigned", queryParams!);
        var response = await HttpClient.GetAsync(url, cancellationToken);
        return await ReadAsAsync<PaginatedResult<JudgeAssignedIdeaDto>>(response);
    }
}
